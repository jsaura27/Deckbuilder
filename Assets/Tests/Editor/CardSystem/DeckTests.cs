using NUnit.Framework;

namespace Deckbuilder.Tests.Editor.CardSystem
{
    public class DeckTests
    {
        [Test]
        public void DrawFromEmptyDeckReturnsNull()
        {
            var deck = new Deck();
            Assert.IsNull(deck.Draw());
        }
    }
}
