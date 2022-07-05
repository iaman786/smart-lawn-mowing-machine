namespace slmm.LawnMowing.Api.Service
{
    using System;
    using System.Threading.Tasks;
    using SmartLawnMowing.Domain.Exceptions;
    using SmartLawnMowing.Domain.Model;
    using SmartLawnMowing.Domain.Presentation;
    using SmartLawnMowing.Domain.Presentation.Views;

    public class MowerService
    {
        private Mower mower;

        private Mower Mower
        {
            get
            {
                if (mower == null)
                {
                    mower = _mowerFactory.Create();
                }

                return mower;
            }
        }

        private readonly IMowerFactory _mowerFactory;

        public MowerService(IMowerFactory mowerFactory)
        {
            _mowerFactory = mowerFactory;
        }

        public async Task<MowerResponse> Turn(string direction)
        {
            return await Task.Run(() =>
            {
                if (!Enum.TryParse<Rotate>(direction, out var turnDirection))
                {
                    return MowerResponse.InvalidInput;
                }

                this.Mower.Turn(turnDirection);
                    return MowerResponse.Success;
            });
        }

        public async Task<MowerResponse> Move()
        {
            return await Task.Run(() =>
            {
                try
                {
                    this.Mower.Move();
                    return MowerResponse.Success;
                }
                catch(OutOfRangeException)
                {
                    return MowerResponse.OutOfRange;
                }
            });
        }

        public async Task<PositionView> GetPosition()
        {
            return await Task.Run(() => this.Mower.GetPosition().ToView());
        }
    }
}
