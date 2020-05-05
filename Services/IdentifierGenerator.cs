using System.Threading;

namespace AkkaTestApi.Services
{
    public class IdentifierGenerator
    {
        private int _id;

        public int GenerateId() => Interlocked.Increment(ref _id);
    }
}