using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Common
{
    static class TreePanelHelper
    {
        public static void Tree_Folder_Toggle(IWebDriver driver, IWebElement folderEleToExpandOrCollapse, bool isExpand)
        {

            var parent = folderEleToExpandOrCollapse;

            //https://developer.mozilla.org/en-US/docs/Web/XPath/Axes
            IWebElement treeExpanderBtn = parent.FindElement(By.XPath("preceding-sibling::*"));

            var mainTreeNode_li = treeExpanderBtn.FindElement(By.XPath(".."));

            string classStr = mainTreeNode_li.GetAttribute("class");

            if (isExpand)
            {
                //jstree-closed   if this class is there then it is collapsed
                if (classStr.Contains("jstree-closed"))
                {
                    treeExpanderBtn.Click();

                    classStr = mainTreeNode_li.GetAttribute("class");
                    if (classStr.Contains("jstree-closed"))
                        treeExpanderBtn.Click();
                    DriverHelpers.WaitForSometime(driver);
                }
            }
            else
            {
                //jstree-open   if this class is there then it is expanded hence we must collapse
                if (classStr.Contains("jstree-open"))
                {
                    treeExpanderBtn.Click();
                    DriverHelpers.WaitForSometime(driver);
                }
            }
        }

        public static IWebElement Tree_SubFolder_Toggle(IWebDriver driver, IWebElement parentFolder, string folderName, bool isExpand)
        {
            // var treeDiv_treeWrapper = driver.FindElement(By.Id("treeWrapper"));

            //http://stackoverflow.com/questions/27496980/xpath-to-find-the-first-child-if-a-sibling-contains-certain-text
            //---- //ul[contains(li,'Sample')]/li[1]
            //http://stackoverflow.com/questions/30407106/getting-next-sibling-element-using-xpath-and-selenium-for-java
            //string xPathForProjName = string.Format("./ul/li/a[contains(nobr,'{0}')]/a[1]", projectName);


            string xPathForProjName = string.Format("./ul/li/a/nobr[text()='{0}']", folderName);
            var nobr_Tag = parentFolder.FindElement(By.XPath(xPathForProjName));

            var parent = nobr_Tag.FindElement(By.XPath(".."));

            //https://developer.mozilla.org/en-US/docs/Web/XPath/Axes
            IWebElement treeExpanderBtn = parent.FindElement(By.XPath("preceding-sibling::*"));

            var mainTreeNode_li = treeExpanderBtn.FindElement(By.XPath(".."));

            string classStr = mainTreeNode_li.GetAttribute("class");

            if (isExpand)
            {
                //jstree-closed   if this class is there then it is collapsed
                if (classStr.Contains("jstree-closed"))
                {
                    treeExpanderBtn.Click();

                    classStr = mainTreeNode_li.GetAttribute("class");
                    if (classStr.Contains("jstree-closed"))
                        treeExpanderBtn.Click();
                    DriverHelpers.WaitForSometime(driver);
                }
            }
            else
            {
                //jstree-open   if this class is there then it is expanded hence we must collapse
                if (classStr.Contains("jstree-open"))
                {
                    treeExpanderBtn.Click();
                    DriverHelpers.WaitForSometime(driver);
                }
            }

            ////////--now select the module

            //var xPathForModule = string.Format("./ul/li/a/nobr[text()='{0}']", directModuleName);
            //var a_tag_module_text = mainTreeNode_li.FindElement(By.XPath(xPathForModule)).FindElement(By.XPath(".."));

            //a_tag_module_text.Click();

            return mainTreeNode_li;
        }

        public static IWebElement FirstLevel_TreeFolder_Toggle(IWebDriver driver, string folderName, bool isExpand)
        {
            var treeDiv_treeWrapper = driver.FindElement(By.Id("treeWrapper"));

            return Tree_SubFolder_Toggle(driver, treeDiv_treeWrapper, folderName, isExpand);
        }

        /// <summary>
        /// Returns li node in the tree
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static IWebElement FirstLevel_TreeFolder_Expand(IWebDriver driver, string folderName)
        {
            return FirstLevel_TreeFolder_Toggle(driver, folderName, isExpand: true);
        }

        /// <summary>
        /// Returns li node in the tree
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static IWebElement FirstLevel_TreeFolder_Collpase(IWebDriver driver, string folderName)
        {
            return FirstLevel_TreeFolder_Toggle(driver, folderName, isExpand: false);
        }

        public static IWebElement Tree_SelectLeafUnderFolder(IWebDriver driver, IWebElement parentTreeNode, string nodeName)
        {
            var xPathForModule = string.Format("./ul/li/a/nobr[text()='{0}']", nodeName);
            var node = parentTreeNode.FindElement(By.XPath(xPathForModule)).FindElement(By.XPath(".."));

            node.Click();

            DriverHelpers.WaitForSometime(driver);

            return node;
        }

        public static IWebElement Tree_SelectDirectNode(IWebDriver driver, string nodeName)
        {
            var treeDiv_treeWrapper = driver.FindElement(By.Id("treeWrapper"));

            string xPathForProjName = string.Format("./ul/li/a/nobr[text()='{0}']", nodeName);
            var nobr_Tag = treeDiv_treeWrapper.FindElement(By.XPath(xPathForProjName));

            var li_node = nobr_Tag.FindElement(By.XPath(".."));

            li_node.Click();
            
            DriverHelpers.WaitForSometime(driver);

            return li_node;
        }

        public static IWebElement Tree_Select_ProjectFolder(IWebDriver driver, int? optionalProjectId = null)
        {
            var treeDiv_treeWrapper = driver.FindElement(By.Id("treeWrapper"));
            //   Modules/PROJECT/ProjectInfo.aspx?pid=872&Context=PROJECT&InstanceID=0&Mode=View

            var aEleList = treeDiv_treeWrapper.FindElements(By.XPath("./ul/li/a"));

            IWebElement returnAnchorTag = null;

            var signature = "PROJECT/ProjectInfo.aspx".ToLower();

            foreach (var aItem in aEleList)
            {
                string hrefValue = aItem.GetAttribute("href").ToLower();
                if (hrefValue.Contains(signature))
                {
                    if (!optionalProjectId.HasValue)
                    {
                        returnAnchorTag = aItem;
                        break;
                    }
                    else
                    {
                        if (hrefValue.Contains("pid=" + optionalProjectId.Value))
                        {
                            returnAnchorTag = aItem;
                            break;
                        }
                    }
                }
            }

            if (returnAnchorTag != null)
            {
                return Tree_SubFolder_Toggle(driver, treeDiv_treeWrapper, returnAnchorTag.Text.Trim(), isExpand: true);

                //IWebElement treeExpanderBtn = returnAnchorTag.FindElement(By.XPath("preceding-sibling::*"));

                //var mainTreeNode_li = treeExpanderBtn.FindElement(By.XPath(".."));
                //return mainTreeNode_li;
            }

            throw new Exception("Project Not found.");
        }
    }
}
