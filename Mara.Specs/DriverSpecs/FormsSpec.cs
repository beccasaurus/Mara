using System;
using NUnit.Framework;
using Mara;

namespace Mara.DriverSpecs {

    /*
     * The spec covers the basics of filling in and submitting forms
     *
     *   FillIn("...", "...")
     *   FillInFields("...", "...")
     *   ClickButton("...")
     */
    [TestFixture]
    public class FormsSpec : MaraTest {

        [SetUp] public void Setup(){ Visit("/Form.aspx"); }

        [Test]
        public void TheExampleFormShouldBeSetupLikeWeThinkItIs() {
            // #DogName should exist and value should be empty (it does not have a NAME attribute)
            Assert.Null(    Find("//*[@name='DogName']")  );
            Assert.NotNull( Find("id('DogName')")         );
            Assert.IsEmpty( Find("id('DogName')").Value   );

            // #DogBreed should not exist, it's an input with NAME="DogBreed" and value should be empty
            Assert.Null(    Find("id('DogBreed')")              );
            Assert.NotNull( Find("//*[@name='DogBreed']")       );
            Assert.IsEmpty( Find("//*[@name='DogBreed']").Value );

            // Button with VALUE="POST some stuff" should exist
            Assert.NotNull( Find("//input[@type='submit'][@value='POST some stuff']") );
        }

        [Test][Ignore("finish the setup [Test] first ...")]
        public void FillIn_LooksForFieldWithIdOrName_BlowsUpIfNoneFound() {
        }

        [Test][Ignore("do the FillIn_ [Test] first ...")]
        public void CanFillInAField_BlowsUpIfFieldNotDefined() {
            Assert.NotNull(Find("//*[@name='DogName']"));
            Assert.That(Find("//*[@name='DogName']").Value, Is.EqualTo("")); // empty value

            FillIn("DogName", "Snoopy");
            // ....
        }

        [Test][Ignore]
        public void CanFillInMultipleFields_BlowsUpIfAnyAreNotDefined() {

        }
    }
}
