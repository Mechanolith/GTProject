//Normally I wouldn't make separate classes for this and incoming block data.
//But for my own sanity I must standardise the variable name formatting, so only this class exists to convert the names.
[System.Serializable]
public class BlockData
{
    int id;
    string subject;
    string grade;
    MasteryLevel mastery;
    string domainID;
    string domain;
    string cluster;
    string standardID;
    string standardDescription;

    public BlockData(IncomingBlockData _incomingData)
    {
        id = _incomingData.id;
        subject = _incomingData.subject;
        grade = _incomingData.grade;
        mastery = _incomingData.mastery;
        domainID = _incomingData.domainid;
        domain = _incomingData.domain;
        cluster = _incomingData.cluster;
        standardID = _incomingData.standardid;
        standardDescription = _incomingData.standarddescription;
    }
}
