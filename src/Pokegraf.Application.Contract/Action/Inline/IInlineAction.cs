namespace Pokegraf.Application.Contract.Action.Inline
{
    public interface IInlineAction : IBotAction
    {
        string Query { get; set; }
        string Offset { get; set; }
    }
}