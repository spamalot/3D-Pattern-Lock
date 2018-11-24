
public class PinServerController : TechniqueServerController
{
    public override void OnButtonPress(string text) {
        EnteredNumbers.Add(int.Parse(text));
        InvokeOnEnteredNumbersChanged();
    }
}
