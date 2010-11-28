using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Mara;

using OpenQA.Selenium;

namespace Mara.Drivers {
    public partial class WebDriver : IDriver {

        /*
         * WebDriver.Element
         *
         * WebDriver's implementation of Mara.IElement
         */
        public class Element : IElement {

            IWebElement NativeElement { get; set; }

            internal static List<IElement> List(ReadOnlyCollection<IWebElement> nativeElements) {
                var list = new List<IElement>();
                foreach (var webElement in nativeElements)
                    list.Add(new Element(webElement));
                return list;
            }

            internal Element(IWebElement nativeElement) {
                NativeElement = nativeElement;
            }

            public void Click() {
                NativeElement.Click();
            }

            public string Value {
                get { return NativeElement.Value; }
            }

            public string Text {
                get { return NativeElement.Text; }
            }

            public string this[string attributeName] {
                get { return NativeElement.GetAttribute(attributeName); }
            }
        }
    }
}
