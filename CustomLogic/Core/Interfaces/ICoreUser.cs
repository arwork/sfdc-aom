namespace CustomLogic.Core.Interfaces
{
    /// <summary>
    /// Some basic user information will need to be decieded on each project
    /// </summary>
    public interface ICoreUser 
    {
        int OrganisationId { get; set; }
        string Id { get; set; }
    }
}
