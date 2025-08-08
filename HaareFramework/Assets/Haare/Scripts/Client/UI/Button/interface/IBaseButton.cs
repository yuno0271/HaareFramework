using R3;

namespace Haare.Client.UI.BaseButton
{
    public interface IBaseButton
    {
        Subject<Unit> Onclicked	{ get; }
        Subject<Unit> Onhovered	{ get; }
        Subject<Unit> Onexited	{ get; }
    }
}