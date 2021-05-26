namespace API.Entities
{

    /// |AppUser| -----*hasone*------|SourceUser|------*-withmany*------|LikedUsers|
    /// |AppUser| -----*hasone*------|LikedUser|--------*withmany*------|LikedByUsers|
    public class UserLike
    {
        public AppUser SourceUser { get; set; } // the user who is liking other user id
        public int SourceUserId { get; set; }
        public AppUser LikedUser { get; set; } // who is being liked by Souce User
        public int LikedUserId { get; set; }
    }
}