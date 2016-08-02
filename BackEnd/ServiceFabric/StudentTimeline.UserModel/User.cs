namespace StudentTimeline.UserModel
{
    public class User
    {
        UserId _Id = new UserId();
        /// <summary>
        /// Unique identifier for each user
        /// </summary>
        public UserId Id {
            get {
                return _Id;
            }
            set
            {
                _Id = value; ;
            }
        }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User Blurb
        /// </summary>
        public string Blurb { get; set; }

        /// <summary>
        /// Url to the User's Profile Image
        /// </summary>
        public string ProfileImageUrl { get; set; }
    }
}
