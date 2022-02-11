using NUnit.Framework;
using static TermoLib.CharStatus;

namespace TermoLib.Tests
{
    public class GameEngineTest
    {

        [Test]
        [TestCase("pasta", NotOk, NotOk, AllOk, AllOk, NotOk)]
        [TestCase("atroz", NotOk, OnlyCharOk, NotOk, NotOk, NotOk)]
        [TestCase("entes", OnlyCharOk, NotOk, OnlyCharOk, OnlyCharOk, OnlyCharOk)]
        [TestCase("varal", NotOk, NotOk, NotOk, NotOk, NotOk)]
        [TestCase("hotel", NotOk, NotOk, OnlyCharOk, OnlyCharOk, NotOk)]
        [TestCase("tente", AllOk, AllOk, NotOk, AllOk, AllOk)]
        [TestCase("trote", AllOk, NotOk, NotOk, AllOk, AllOk)]
        [TestCase("longe", NotOk, NotOk, NotOk, NotOk, AllOk)]
        [TestCase("perto", NotOk, AllOk, NotOk, AllOk, NotOk)]
        [TestCase("estes", OnlyCharOk, OnlyCharOk, OnlyCharOk, OnlyCharOk, OnlyCharOk)]
        public void Should_return_correct_feedback(
            string typed, CharStatus cs1, CharStatus cs2, CharStatus cs3, CharStatus cs4, CharStatus cs5)
        {
            var engine = new GameEngine("teste");
            var feedback = engine.Verify(typed);

            TestCharPosition(0, typed[0], cs1, feedback);
            TestCharPosition(1, typed[1], cs2, feedback);
            TestCharPosition(2, typed[2], cs3, feedback);
            TestCharPosition(3, typed[3], cs4, feedback);
            TestCharPosition(4, typed[4], cs5, feedback);

            Assert.IsFalse(feedback.IsWinner());
        }

        [Test]
        public void Should_return_correct_feedback_correct_word()
        {
            var engine = new GameEngine("teste");
            var feedback = engine.Verify("teste");

            TestCharPosition(0, 't', AllOk, feedback);
            TestCharPosition(1, 'e', AllOk, feedback);
            TestCharPosition(2, 's', AllOk, feedback);
            TestCharPosition(3, 't', AllOk, feedback);
            TestCharPosition(4, 'e', AllOk, feedback);

            Assert.IsTrue(feedback.IsWinner());
        }


        [Test]
        public void Should_validate_invalid_word()
        {
            var engine = new GameEngine("teste");

            Assert.IsTrue(engine.InvalidWord(null));
            Assert.IsTrue(engine.InvalidWord(""));
            Assert.IsTrue(engine.InvalidWord("  "));
            Assert.IsTrue(engine.InvalidWord("abc"));
            Assert.IsTrue(engine.InvalidWord("abcdefghij"));
            Assert.IsTrue(engine.InvalidWord("12345"));
            Assert.IsTrue(engine.InvalidWord("?:>[a"));
        }

        
        private void TestCharPosition(int index, char c, CharStatus status, Feedback feedback)
        {
            Assert.AreEqual(status, feedback.Chars[index].CharStatus);
            Assert.AreEqual(c, feedback.Chars[index].Char);
        }
    }


    
}