using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Entities
{
	public class UserPublicData
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public bool WantsToRecieveNotifications { get; set; } = true;

		public int UserId { get; set; }
		public virtual User User { get; set; }

		public virtual Image ProfileImage { get; set; }


	}
}
