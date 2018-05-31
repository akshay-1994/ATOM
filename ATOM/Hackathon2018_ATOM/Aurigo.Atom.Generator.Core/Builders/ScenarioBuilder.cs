using System;
using System.Collections.Generic;
using System.Linq;
using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Generator.Core.Helpers;

namespace Aurigo.Atom.Generator.Core.Builders
{
    public class ScenarioBuilder
    {
        public List<TestCaseComponentGroup> TestCaseGroups { get; set; } = new List<TestCaseComponentGroup>();
        private Dictionary<string, int> maxControlOrder = new Dictionary<string, int>();
        private TestModuleConfig _testModuleConfig;

        public ScenarioBuilder()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioBuilder"/> class.
        /// </summary>
        /// <param name="testCaseGroups">The test case groups.</param>
        /// <param name="testModuleConfig">The test module configuration.</param>
        public ScenarioBuilder(List<TestCaseComponentGroup> testCaseGroups, TestModuleConfig testModuleConfig) : this()
        {
            TestCaseGroups = testCaseGroups;
            _testModuleConfig = testModuleConfig;
        }

        public List<ScenarioDTO> Build()
        {
            PrepareTestCaseGroups();
            List<ScenarioDTO> scenarios = new List<ScenarioDTO>();
            scenarios.AddRange(GeneratePositiveTestScenarios());
            scenarios.AddRange(GenerateNegativeTestScenarios());

            return scenarios;
        }

        private void PrepareTestCaseGroups()
        {
            foreach (TestCaseComponentGroup group in TestCaseGroups)
            {
                maxControlOrder.Add(group.ControlName, 0);
                OrderTestCases(group, TestCaseType.POSITIVE);
                OrderTestCases(group, TestCaseType.DEFAULT);
                OrderTestCases(group, TestCaseType.NEGATIVE);
            }
        }

        private void OrderTestCases(TestCaseComponentGroup groupInFocus, TestCaseType typeOfTestCase)
        {
            var filteredTestCases = FilterTestCases(groupInFocus.TestCaseComponents, typeOfTestCase);
            if (filteredTestCases != null)
            {
                int index = maxControlOrder[groupInFocus.ControlName];
                filteredTestCases.OrderBy((tc) => tc.Name).ToList().ForEach((tc) =>
                {
                    tc.Order = index;
                    index++;
                });
                maxControlOrder[groupInFocus.ControlName] = index;
            }
        }

        private List<ScenarioDTO> GeneratePositiveTestScenarios()
        {
            int maxNumberTestCases = GetMaximumNumberOfTestCases(TestCaseTypeHelper.PositiveTestCaseTypes);

            List<ScenarioDTO> scenarios = new List<ScenarioDTO>();
            int currentIndex = 0;
            while (currentIndex < maxNumberTestCases)
            {
                ScenarioDTO scenario = new ScenarioDTO
                {
                    Setters = new List<string>(),
                    DBValidators = new List<string>(),
                    EditModeValidators = new List<string>(),
                    ViewModeValidators = new List<string>(),
                    AutomationGuidFieldName = _testModuleConfig.AutomationGuidFieldName,
                    ScenarioDescription = new List<string>(),
                    OnScreenValidators = new List<string>(),
                    IsSaveWillSucceed = true
                }
                    ;
                scenario.Name = "POSITIVE_" + (currentIndex + 1);
                foreach (TestCaseComponentGroup group in TestCaseGroups)
                {
                    var filteredTestCases = FilterTestCases(group.TestCaseComponents, TestCaseTypeHelper.PositiveTestCaseTypes);
                    if (filteredTestCases != null && filteredTestCases.Count > 0)
                    {
                        var firstTestCase = GetLRUPositiveTestCase_IncrementCounters(group, filteredTestCases);
                        BuildScenario(scenario, firstTestCase);

                        if (!firstTestCase.WillSaveSucceed)
                            scenario.IsSaveWillSucceed = false;
                    }
                }
                currentIndex++;
                scenarios.Add(scenario);
            }
            return scenarios;
        }

        /// <summary>
        /// Builds the scenario.
        /// </summary>
        /// <param name="scenario">The scenario.</param>
        /// <param name="testCaseComponent">The test case component.</param>
        private void BuildScenario(ScenarioDTO scenario, TestCaseComponent testCaseComponent)
        {
            if (!string.IsNullOrEmpty(testCaseComponent.TestCaseSetter))
                scenario.Setters.Add(testCaseComponent.TestCaseSetter);
            if (!string.IsNullOrEmpty(testCaseComponent.TestCaseDBValidator))
                scenario.DBValidators.Add(testCaseComponent.TestCaseDBValidator);
            if (!string.IsNullOrEmpty(testCaseComponent.TestCaseEditModeValidator))
                scenario.EditModeValidators.Add(testCaseComponent.TestCaseEditModeValidator);
            if (!string.IsNullOrEmpty(testCaseComponent.TestCaseViewModeValidator))
                scenario.ViewModeValidators.Add(testCaseComponent.TestCaseViewModeValidator);

            if (!string.IsNullOrEmpty(testCaseComponent.OnScreenValidator))
                scenario.OnScreenValidators.Add(testCaseComponent.OnScreenValidator);

            if (!string.IsNullOrEmpty(testCaseComponent.Description))
                scenario.ScenarioDescription.Add(testCaseComponent.Description);
        }

        private TestCaseComponent GetLRUPositiveTestCase_IncrementCounters(TestCaseComponentGroup groupInfocus, List<TestCaseComponent> testCases)
        {
            var firstTestCase = testCases.OrderBy((tc) => tc.Order).First();
            firstTestCase.Order = maxControlOrder[groupInfocus.ControlName] + 1;
            maxControlOrder[groupInfocus.ControlName]++;
            return firstTestCase;
        }

        private List<ScenarioDTO> GenerateNegativeTestScenarios()
        {
            List<ScenarioDTO> scenarios = new List<ScenarioDTO>();

            int currentIndex = 0;

            foreach (TestCaseComponentGroup group in TestCaseGroups)
            {
                List<TestCaseComponent> filteredTestCases = FilterTestCases(group.TestCaseComponents, TestCaseTypeHelper.NegativeTestCaseTypes);
                if (filteredTestCases != null && filteredTestCases.Count > 0)
                {
                    foreach (TestCaseComponent negativeTestCase in filteredTestCases)
                    {
                        ScenarioDTO scenario = new ScenarioDTO()
                        {
                            Setters = new List<string>(),
                            DBValidators = new List<string>(),
                            EditModeValidators = new List<string>(),
                            ViewModeValidators = new List<string>(),
                            AutomationGuidFieldName = _testModuleConfig.AutomationGuidFieldName,
                            ScenarioDescription = new List<string>(),
                            OnScreenValidators = new List<string>(),
                            IsSaveWillSucceed = false
                        };
                        scenario.Name = "NEGATIVE_" + ++currentIndex;
                        var testCaseComponents = GenerateNegativeTestCases(group, negativeTestCase);

                        testCaseComponents.ForEach(tcc =>
                        {
                            BuildScenario(scenario, tcc);
                        });
                        scenarios.Add(scenario);
                    }
                }
            }

            return scenarios;
        }

        private List<TestCaseComponent> GenerateNegativeTestCases(TestCaseComponentGroup groupInFocus, TestCaseComponent negativeTestCase)
        {
            List<TestCaseComponent> testCases = new List<TestCaseComponent>();

            foreach (TestCaseComponentGroup group in TestCaseGroups)
            {
                if (group.ControlName == groupInFocus.ControlName)
                {
                    testCases.Add(negativeTestCase);
                }
                else
                {
                    List<TestCaseComponent> positiveTestCases = FilterTestCases(group.TestCaseComponents, TestCaseTypeHelper.PositiveTestCaseTypes);
                    var firstTestCase = GetLRUPositiveTestCase_IncrementCounters(group, positiveTestCases);
                    testCases.Add(firstTestCase);
                }
            }

            return testCases;
        }

        private int GetMaximumNumberOfTestCases(params TestCaseType[] types)
        {
            int max = 0;
            foreach (TestCaseComponentGroup group in TestCaseGroups)
            {
                var filteredTestCases = FilterTestCases(group.TestCaseComponents, types);
                var numberOfTestCases = filteredTestCases.Count();
                max = Math.Max(max, numberOfTestCases);
            }
            return max;
        }

        private List<TestCaseComponent> FilterTestCases(List<TestCaseComponent> testCaseComponents, params TestCaseType[] types)
        {
            List<TestCaseComponent> filteredList = new List<TestCaseComponent>();

            filteredList = testCaseComponents.Where((tc) => { return types.Contains(tc.Type); }).ToList();

            return filteredList;
        }
    }
}