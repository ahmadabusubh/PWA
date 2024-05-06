using PWAX.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWAX.data.SqlServerEF
{
    public class ContactEntity : IdataHelper<Contact>
    {
        private readonly List<Contact> _contacts;
        public ContactEntity()
        {
            _contacts = new List<Contact>();
        }

        public int Add(Contact contact )
        {
            _contacts.Add(contact);
            return 1;
        }

        public int Delete(int Id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == Id);
            if (contact != null)
            {
                _contacts.Remove(contact);
                return 1;
            }
            return 0;
        }

        public int Edit(Contact contact, int Id)
        {
            var existingContact = _contacts.FirstOrDefault(x => x.Id == Id);
            if (existingContact != null) {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;
                return 1;



            }
            return 0;
        }

        public Contact Find(int Id)
        {
            return _contacts.FirstOrDefault(x => x.Id == Id);
        }

        public List<Contact> GetAllData()
        {
            return _contacts;
        }

        public List<Contact> GetDataByUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public List<Contact> Search(string SerchItem)
        {
            return _contacts.Where(c =>
                            c.FirstName.Contains(SerchItem) ||
                            c.LastName.Contains(SerchItem) ||
                            c.Email.Contains(SerchItem) ||
                            c.PhoneNumber.Contains(SerchItem)
                        ).ToList();
        }
    }
}
