namespace BDS.Communication.Responses.Errors;

public class ResponseErrorJson
{
    public IList<string> Errors { get; private set; }
    public ResponseErrorJson(IList<string> errors)
    {
        Errors = errors;
    }
    public ResponseErrorJson(string error)
    {
        Errors = new List<string>
        {
            error
        };
    }
}