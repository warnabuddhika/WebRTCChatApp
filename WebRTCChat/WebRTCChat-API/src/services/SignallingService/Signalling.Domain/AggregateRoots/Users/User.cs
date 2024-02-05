using Common.Domain.Auditing;
using Common.Domain.Enums;
using Users.Domain.Enums;

namespace Users.Domain.AggregateRoots.Users;

    public class User : AuditableAggregateRoot<Guid>, ISoftDelete
    {
        private User(Guid id, string firstName, string email)
           : base(id)
        {
            FirstName = firstName;
            Email = email;
        }
        public virtual string FirstName { get; private set; }
        public virtual string LastName { get; private set; }
        public virtual string Email { get; private set; }
        public virtual UserStatus UserStatus { get; private set; }

	public virtual Guid IdentityId { get; private set; }

	public bool IsDeleted { get; private set; }

	public static User Create(
            string firstName,
            string lastName,
		Guid identityId,
            string email)
        {
            var User = new User(Guid.NewGuid(), firstName, email)
            {
                LastName = lastName,
                Email = email,
			IdentityId = identityId,
			UserStatus =  UserStatus.Active
               
            };

            return User;
        }

        public void UpdateUser(
            string firstName,
            string lastName,
		UserStatus userStatus
            )
        {
            FirstName = firstName;
            LastName = lastName;
		UserStatus = userStatus;
        }

    }
