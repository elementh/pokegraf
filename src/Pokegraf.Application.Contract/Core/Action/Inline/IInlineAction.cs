namespace Pokegraf.Application.Contract.Core.Action.Inline
{
    public interface IInlineAction : IBotAction
    {
        string Query { get; set; }
        string Offset { get; set; }
    }
}