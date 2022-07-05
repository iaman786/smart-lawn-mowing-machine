namespace slmm.LawnMowing.Api.Service
{
    using SmartLawnMowing.Domain.Model;

    public interface IMowerFactory
    {
        Mower Create();
    }
}
