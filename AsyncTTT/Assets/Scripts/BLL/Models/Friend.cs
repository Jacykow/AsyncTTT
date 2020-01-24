using Assets.Scripts.BLL.Enums;

namespace Assets.Scripts.BLL.Models
{
    public class Friend
    {
        public string Name { get; set; }
        public FriendState State { get; set; }

        public int Order => State == FriendState.Invitation ? 1 : 0;
    }
}
