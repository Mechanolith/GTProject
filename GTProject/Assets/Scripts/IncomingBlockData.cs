//Normally I wouldn't make a separate class for the incoming block data.
//But for my own sanity I must standardise the variable name formatting, so only this class matches the JSON.
public class IncomingBlockData
{
    public int id;
    public string subject;
    public string grade;
    public MasteryLevel mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}
