using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurigo.Atom.Common.DTO;
using System.Collections.Generic;
using Aurigo.Atom.Generator.Core.Builders;

namespace Aurigo.Atom.Generator.Core.UnitTests
{
    [TestClass]
    public class UnitTest_ScenarioBuilder
    {
        [TestMethod]
        public void Simple()
        {
            List<ScenarioDTO> expected_scenarios = new List<ScenarioDTO>();
            List<TestCaseComponentGroup> groups = BuildData_Simple(ref expected_scenarios);
            TestModuleConfig testModuleConfig = new TestModuleConfig();
            ScenarioBuilder builder = new ScenarioBuilder(groups, testModuleConfig);
            List<ScenarioDTO> actual_scenarios = builder.Build();

            CollectionAssert.AreEqual(expected_scenarios, actual_scenarios, new TestCaseScenarioComparer());
        }

        private List<TestCaseComponentGroup> BuildData_Simple(ref List<ScenarioDTO> expected_scenarios)
        {
            List<TestCaseComponentGroup> groups = new List<TestCaseComponentGroup>();
            TestCaseComponentGroup firstControl = new TestCaseComponentGroup();
            firstControl.ControlName = "FirstControl";

            List<TestCaseComponent> firstControl_Components = new List<TestCaseComponent>();
            TestCaseComponent firstControl_Component1 = new TestCaseComponent();
            firstControl_Component1.Name = "firstControl_Component1";
            firstControl_Component1.Type = TestCaseType.POSITIVE;
            firstControl_Components.Add(firstControl_Component1);

            TestCaseComponent firstControl_Component2 = new TestCaseComponent();
            firstControl_Component2.Name = "firstControl_Component2";
            firstControl_Component2.Type = TestCaseType.POSITIVE;
            firstControl_Components.Add(firstControl_Component2);

            TestCaseComponent firstControl_Component3 = new TestCaseComponent();
            firstControl_Component3.Name = "firstControl_Component3";
            firstControl_Component3.Type = TestCaseType.NEGATIVE;
            firstControl_Components.Add(firstControl_Component3);

            TestCaseComponent firstControl_Component4 = new TestCaseComponent();
            firstControl_Component4.Name = "firstControl_Component4";
            firstControl_Component4.Type = TestCaseType.DEFAULT;
            firstControl_Components.Add(firstControl_Component4);

            firstControl.TestCaseComponents = firstControl_Components;

            groups.Add(firstControl);

            TestCaseComponentGroup secondControl = new TestCaseComponentGroup();
            secondControl.ControlName = "secondControl";

            List<TestCaseComponent> secondControl_Components = new List<TestCaseComponent>();
            TestCaseComponent secondControl_Component1 = new TestCaseComponent();
            secondControl_Component1.Name = "secondControl_Component1";
            secondControl_Component1.Type = TestCaseType.POSITIVE;
            secondControl_Components.Add(secondControl_Component1);

            TestCaseComponent secondControl_Component2 = new TestCaseComponent();
            secondControl_Component2.Name = "secondControl_Component2";
            secondControl_Component2.Type = TestCaseType.DEFAULT;
            secondControl_Components.Add(secondControl_Component2);

            TestCaseComponent secondControl_Component3 = new TestCaseComponent();
            secondControl_Component3.Name = "secondControl_Component3";
            secondControl_Component3.Type = TestCaseType.NEGATIVE;
            secondControl_Components.Add(secondControl_Component3);

            secondControl.TestCaseComponents = secondControl_Components;

            groups.Add(secondControl);

            ScenarioDTO scenario1 = new ScenarioDTO();
            scenario1.Name = "POSITIVE_1";
            BuildScenario(scenario1, firstControl_Component1);
            BuildScenario(scenario1, secondControl_Component1);
            expected_scenarios.Add(scenario1);

            ScenarioDTO scenario2 = new ScenarioDTO();
            scenario2.Name = "POSITIVE_2";
            BuildScenario(scenario2, firstControl_Component2);
            BuildScenario(scenario2, secondControl_Component2);
            expected_scenarios.Add(scenario2);

            ScenarioDTO scenario3 = new ScenarioDTO();
            scenario3.Name = "POSITIVE_3";
            BuildScenario(scenario3, firstControl_Component4);
            BuildScenario(scenario3, secondControl_Component1);
            expected_scenarios.Add(scenario3);

            ScenarioDTO scenario4 = new ScenarioDTO();
            scenario4.Name = "NEGATIVE_1";
            BuildScenario(scenario4, firstControl_Component3);
            BuildScenario(scenario4, secondControl_Component2);
            expected_scenarios.Add(scenario4);

            ScenarioDTO scenario5 = new ScenarioDTO();
            scenario5.Name = "NEGATIVE_2";
            BuildScenario(scenario5, firstControl_Component1);
            BuildScenario(scenario5, secondControl_Component3);
            expected_scenarios.Add(scenario5);

            return groups;
        }

        [TestMethod]
        public void FourControls()
        {
            List<ScenarioDTO> expected_scenarios = new List<ScenarioDTO>();
            List<TestCaseComponentGroup> groups = BuildData_FourControls(ref expected_scenarios);
            var testModuleConfig = new TestModuleConfig();
            ScenarioBuilder builder = new ScenarioBuilder(groups, testModuleConfig);
            List<ScenarioDTO> actual_scenarios = builder.Build();

            CollectionAssert.AreEqual(expected_scenarios, actual_scenarios, new TestCaseScenarioComparer());
        }

        private List<TestCaseComponentGroup> BuildData_FourControls(ref List<ScenarioDTO> expected_scenarios)
        {
            List<TestCaseComponentGroup> groups = new List<TestCaseComponentGroup>();
            TestCaseComponentGroup C1 = new TestCaseComponentGroup();
            C1.ControlName = "C1";

            TestCaseComponent C1_P1 = new TestCaseComponent();
            C1_P1.Name = "C1_P1";
            C1_P1.Type = TestCaseType.POSITIVE;
            C1.TestCaseComponents.Add(C1_P1);

            TestCaseComponent C1_P2 = new TestCaseComponent();
            C1_P2.Name = "C1_P2";
            C1_P2.Type = TestCaseType.POSITIVE;
            C1.TestCaseComponents.Add(C1_P2);

            TestCaseComponent C1_D1 = new TestCaseComponent();
            C1_D1.Name = "C1_D1";
            C1_D1.Type = TestCaseType.DEFAULT;
            C1.TestCaseComponents.Add(C1_D1);

            TestCaseComponent C1_N1 = new TestCaseComponent();
            C1_N1.Name = "C1_N1";
            C1_N1.Type = TestCaseType.NEGATIVE;
            C1.TestCaseComponents.Add(C1_N1);

            TestCaseComponent C1_N2 = new TestCaseComponent();
            C1_N2.Name = "C1_N2";
            C1_N2.Type = TestCaseType.NEGATIVE;
            C1.TestCaseComponents.Add(C1_N2);

            groups.Add(C1);

            TestCaseComponentGroup C2 = new TestCaseComponentGroup();
            C2.ControlName = "C2";

            TestCaseComponent C2_P1 = new TestCaseComponent();
            C2_P1.Name = "C2_P1";
            C2_P1.Type = TestCaseType.POSITIVE;
            C2.TestCaseComponents.Add(C2_P1);

            TestCaseComponent C2_N1 = new TestCaseComponent();
            C2_N1.Name = "C2_N1";
            C2_N1.Type = TestCaseType.NEGATIVE;
            C2.TestCaseComponents.Add(C2_N1);

            groups.Add(C2);

            TestCaseComponentGroup C3 = new TestCaseComponentGroup();
            C3.ControlName = "C3";

            TestCaseComponent C3_P1 = new TestCaseComponent();
            C3_P1.Name = "C3_P1";
            C3_P1.Type = TestCaseType.POSITIVE;
            C3.TestCaseComponents.Add(C3_P1);

            TestCaseComponent C3_P2 = new TestCaseComponent();
            C3_P2.Name = "C3_P2";
            C3_P2.Type = TestCaseType.POSITIVE;
            C3.TestCaseComponents.Add(C3_P2);

            TestCaseComponent C3_D1 = new TestCaseComponent();
            C3_D1.Name = "C3_D1";
            C3_D1.Type = TestCaseType.DEFAULT;
            C3.TestCaseComponents.Add(C3_D1);

            TestCaseComponent C3_D2 = new TestCaseComponent();
            C3_D2.Name = "C3_D2";
            C3_D2.Type = TestCaseType.DEFAULT;
            C3.TestCaseComponents.Add(C3_D2);

            TestCaseComponent C3_N1 = new TestCaseComponent();
            C3_N1.Name = "C3_N1";
            C3_N1.Type = TestCaseType.NEGATIVE;
            C3.TestCaseComponents.Add(C3_N1);

            TestCaseComponent C3_N2 = new TestCaseComponent();
            C3_N2.Name = "C3_N2";
            C3_N2.Type = TestCaseType.NEGATIVE;
            C3.TestCaseComponents.Add(C3_N2);

            groups.Add(C3);

            TestCaseComponentGroup C4 = new TestCaseComponentGroup();
            C4.ControlName = "C4";

            TestCaseComponent C4_P1 = new TestCaseComponent();
            C4_P1.Name = "C4_P1";
            C4_P1.Type = TestCaseType.POSITIVE;
            C4.TestCaseComponents.Add(C4_P1);

            TestCaseComponent C4_N1 = new TestCaseComponent();
            C4_N1.Name = "C3_N1";
            C4_N1.Type = TestCaseType.NEGATIVE;
            C4.TestCaseComponents.Add(C4_N1);

            TestCaseComponent C4_N2 = new TestCaseComponent();
            C4_N2.Name = "C4_N2";
            C4_N2.Type = TestCaseType.NEGATIVE;
            C4.TestCaseComponents.Add(C4_N2);

            TestCaseComponent C4_N3 = new TestCaseComponent();
            C4_N3.Name = "C4_N3";
            C4_N3.Type = TestCaseType.NEGATIVE;
            C4.TestCaseComponents.Add(C4_N3);

            TestCaseComponent C4_P2 = new TestCaseComponent();
            C4_P2.Name = "C4_P2";
            C4_P2.Type = TestCaseType.POSITIVE;
            C4.TestCaseComponents.Add(C4_P2);

            groups.Add(C4);

            ScenarioDTO scenario1 = new ScenarioDTO();
            scenario1.Name = "POSITIVE_1";
            BuildScenario(scenario1, C1_P1);
            BuildScenario(scenario1, C2_P1);
            BuildScenario(scenario1, C3_P1);
            BuildScenario(scenario1, C4_P1);
            expected_scenarios.Add(scenario1);

            ScenarioDTO scenario2 = new ScenarioDTO();
            scenario2.Name = "POSITIVE_2";
            BuildScenario(scenario2, C1_P2);
            BuildScenario(scenario2, C2_P1);
            BuildScenario(scenario2, C3_P2);
            BuildScenario(scenario2, C4_P2);
            expected_scenarios.Add(scenario2);

            ScenarioDTO scenario3 = new ScenarioDTO();
            scenario3.Name = "POSITIVE_3";
            BuildScenario(scenario3, C1_D1);
            BuildScenario(scenario3, C2_P1);
            BuildScenario(scenario3, C3_D1);
            BuildScenario(scenario3, C4_P1);
            expected_scenarios.Add(scenario3);

            ScenarioDTO scenario4 = new ScenarioDTO();
            scenario4.Name = "POSITIVE_4";
            BuildScenario(scenario4, C1_P1);
            BuildScenario(scenario4, C2_P1);
            BuildScenario(scenario4, C3_D2);
            BuildScenario(scenario4, C4_P2);
            expected_scenarios.Add(scenario4);

            ScenarioDTO scenario5 = new ScenarioDTO();
            scenario5.Name = "NEGATIVE_1";
            BuildScenario(scenario5, C1_N1);
            BuildScenario(scenario5, C2_P1);
            BuildScenario(scenario5, C3_P1);
            BuildScenario(scenario5, C4_P1);
            expected_scenarios.Add(scenario5);

            ScenarioDTO scenario6 = new ScenarioDTO();
            scenario6.Name = "NEGATIVE_2";
            BuildScenario(scenario6, C1_N2);
            BuildScenario(scenario6, C2_P1);
            BuildScenario(scenario6, C3_P2);
            BuildScenario(scenario6, C4_P2);
            expected_scenarios.Add(scenario6);

            ScenarioDTO scenario7 = new ScenarioDTO();
            scenario7.Name = "NEGATIVE_3";
            BuildScenario(scenario7, C1_P2);
            BuildScenario(scenario7, C2_N1);
            BuildScenario(scenario7, C3_D1);
            BuildScenario(scenario7, C4_P1);
            expected_scenarios.Add(scenario7);

            ScenarioDTO scenario8 = new ScenarioDTO();
            scenario8.Name = "NEGATIVE_4";
            BuildScenario(scenario8, C1_D1);
            BuildScenario(scenario8, C2_P1);
            BuildScenario(scenario8, C3_N1);
            BuildScenario(scenario8, C4_P2);
            expected_scenarios.Add(scenario8);

            ScenarioDTO scenario9 = new ScenarioDTO();
            scenario9.Name = "NEGATIVE_5";
            BuildScenario(scenario9, C1_P1);
            BuildScenario(scenario9, C2_P1);
            BuildScenario(scenario9, C3_N2);
            BuildScenario(scenario9, C4_P1);
            expected_scenarios.Add(scenario9);

            ScenarioDTO scenario10 = new ScenarioDTO();
            scenario10.Name = "NEGATIVE_6";
            BuildScenario(scenario10, C1_P2);
            BuildScenario(scenario10, C2_P1);
            BuildScenario(scenario10, C3_D2);
            BuildScenario(scenario10, C4_N1);
            expected_scenarios.Add(scenario10);

            ScenarioDTO scenario11 = new ScenarioDTO();
            scenario11.Name = "NEGATIVE_7";
            BuildScenario(scenario11, C1_D1);
            BuildScenario(scenario11, C2_P1);
            BuildScenario(scenario11, C3_P1);
            BuildScenario(scenario11, C4_N2);
            expected_scenarios.Add(scenario11);

            ScenarioDTO scenario12 = new ScenarioDTO();
            scenario12.Name = "NEGATIVE_8";
            BuildScenario(scenario12, C1_P1);
            BuildScenario(scenario12, C2_P1);
            BuildScenario(scenario12, C3_P2);
            BuildScenario(scenario12, C4_N3);
            expected_scenarios.Add(scenario12);

            return groups;
        }

        /// <summary>
        /// Builds the scenario.
        /// </summary>
        /// <param name="scenario">The scenario.</param>
        /// <param name="testCaseComponent">The test case component.</param>
        private void BuildScenario(ScenarioDTO scenario, TestCaseComponent testCaseComponent)
        {
            scenario.Setters.Add(testCaseComponent.TestCaseSetter);
            scenario.DBValidators.Add(testCaseComponent.TestCaseDBValidator);
            scenario.EditModeValidators.Add(testCaseComponent.TestCaseEditModeValidator);
            scenario.ViewModeValidators.Add(testCaseComponent.TestCaseViewModeValidator);
            scenario.IsSaveWillSucceed = testCaseComponent.WillSaveSucceed;
        }
    }

    public class TestCaseScenarioComparer : Comparer<ScenarioDTO>
    {
        public override int Compare(ScenarioDTO x, ScenarioDTO y)
        {
            if (x.Name == y.Name)
            {
                if (x.Setters.Count == y.Setters.Count)
                {
                    int isMatching = 1;
                    for (int i = 0; i < x.Setters.Count; i++)
                    {
                        if (x.Setters[i] == y.Setters[i])
                            isMatching = 0;
                        else
                            return 1;
                    }
                    return isMatching;
                }
                else return 1;
            }
            else
                return 1;
        }
    }
}