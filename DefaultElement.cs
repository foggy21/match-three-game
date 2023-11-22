namespace MatchThreeGame
{
    class DefaultElement : Element
    {
        public override void Destroy()
        {
            ElementForm = TypeOfElement.None;
            Button.BackgroundImage = null;
        }

    }
}
