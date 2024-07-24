namespace Progetto_Settimana_2_Manuel.Service
{
    public interface IPasswordEncoder
    {
        string Encode(string password);
        bool IsSame(string plainText, string codedText);
    }
}
