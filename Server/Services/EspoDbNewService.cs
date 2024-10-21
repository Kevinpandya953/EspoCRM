using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using EspoNew.Server.Data;

namespace EspoNew.Server
{
    public partial class EspoDbNewService
    {
        EspoDbNewContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly EspoDbNewContext context;
        private readonly NavigationManager navigationManager;

        public EspoDbNewService(EspoDbNewContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportAccountsaccountsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAccountsaccountsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAccountsaccountsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccount>> GetAccountsaccounts(Query query = null)
        {
            var items = Context.Accountsaccounts.AsQueryable();

            items = items.Include(i => i.employee);
            items = items.Include(i => i.campaign);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAccountsaccountsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAccountsAccountGet(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);
        partial void OnGetAccountsAccountByAccountId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccount> GetAccountsAccountByAccountId(string accountid)
        {
            var items = Context.Accountsaccounts
                              .AsNoTracking()
                              .Where(i => i.account_id == accountid);

            items = items.Include(i => i.employee);
            items = items.Include(i => i.campaign);
 
            OnGetAccountsAccountByAccountId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAccountsAccountGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAccountsAccountCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);
        partial void OnAfterAccountsAccountCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccount> CreateAccountsAccount(EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccount)
        {
            OnAccountsAccountCreated(accountsaccount);

            var existingItem = Context.Accountsaccounts
                              .Where(i => i.account_id == accountsaccount.account_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Accountsaccounts.Add(accountsaccount);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(accountsaccount).State = EntityState.Detached;
                throw;
            }

            OnAfterAccountsAccountCreated(accountsaccount);

            return accountsaccount;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccount> CancelAccountsAccountChanges(EspoNew.Server.Models.EspoDbNew.AccountsAccount item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAccountsAccountUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);
        partial void OnAfterAccountsAccountUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccount> UpdateAccountsAccount(string accountid, EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccount)
        {
            OnAccountsAccountUpdated(accountsaccount);

            var itemToUpdate = Context.Accountsaccounts
                              .Where(i => i.account_id == accountsaccount.account_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(accountsaccount);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAccountsAccountUpdated(accountsaccount);

            return accountsaccount;
        }

        partial void OnAccountsAccountDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);
        partial void OnAfterAccountsAccountDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccount> DeleteAccountsAccount(string accountid)
        {
            var itemToDelete = Context.Accountsaccounts
                              .Where(i => i.account_id == accountid)
                              .Include(i => i.Accountsaccount_contacts)
                              .Include(i => i.Accountsaccount_documents)
                              .Include(i => i.Callscalls)
                              .Include(i => i.Cases_cases)
                              .Include(i => i.Contactscontacts)
                              .Include(i => i.Emailemails)
                              .Include(i => i.Meetingsmeetings)
                              .Include(i => i.Opportunitiesopportunities)
                              .Include(i => i.Taskstasks)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAccountsAccountDeleted(itemToDelete);


            Context.Accountsaccounts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAccountsAccountDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAccountsaccount_contactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccount_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccount_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAccountsaccount_contactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccount_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccount_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAccountsaccount_contactsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>> GetAccountsaccount_contacts(Query query = null)
        {
            var items = Context.Accountsaccount_contacts.AsQueryable();

            items = items.Include(i => i.account);
            items = items.Include(i => i.contact);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAccountsaccount_contactsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAccountsAccountContactGet(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);
        partial void OnGetAccountsAccountContactByAccountContactId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> GetAccountsAccountContactByAccountContactId(string accountcontactid)
        {
            var items = Context.Accountsaccount_contacts
                              .AsNoTracking()
                              .Where(i => i.account_contact_id == accountcontactid);

            items = items.Include(i => i.account);
            items = items.Include(i => i.contact);
 
            OnGetAccountsAccountContactByAccountContactId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAccountsAccountContactGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAccountsAccountContactCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);
        partial void OnAfterAccountsAccountContactCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> CreateAccountsAccountContact(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact accountsaccountcontact)
        {
            OnAccountsAccountContactCreated(accountsaccountcontact);

            var existingItem = Context.Accountsaccount_contacts
                              .Where(i => i.account_contact_id == accountsaccountcontact.account_contact_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Accountsaccount_contacts.Add(accountsaccountcontact);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(accountsaccountcontact).State = EntityState.Detached;
                throw;
            }

            OnAfterAccountsAccountContactCreated(accountsaccountcontact);

            return accountsaccountcontact;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> CancelAccountsAccountContactChanges(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAccountsAccountContactUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);
        partial void OnAfterAccountsAccountContactUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> UpdateAccountsAccountContact(string accountcontactid, EspoNew.Server.Models.EspoDbNew.AccountsAccountContact accountsaccountcontact)
        {
            OnAccountsAccountContactUpdated(accountsaccountcontact);

            var itemToUpdate = Context.Accountsaccount_contacts
                              .Where(i => i.account_contact_id == accountsaccountcontact.account_contact_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(accountsaccountcontact);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAccountsAccountContactUpdated(accountsaccountcontact);

            return accountsaccountcontact;
        }

        partial void OnAccountsAccountContactDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);
        partial void OnAfterAccountsAccountContactDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> DeleteAccountsAccountContact(string accountcontactid)
        {
            var itemToDelete = Context.Accountsaccount_contacts
                              .Where(i => i.account_contact_id == accountcontactid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAccountsAccountContactDeleted(itemToDelete);


            Context.Accountsaccount_contacts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAccountsAccountContactDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAccountsaccount_documentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccount_documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccount_documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAccountsaccount_documentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccount_documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccount_documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAccountsaccount_documentsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>> GetAccountsaccount_documents(Query query = null)
        {
            var items = Context.Accountsaccount_documents.AsQueryable();

            items = items.Include(i => i.account);
            items = items.Include(i => i.document);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAccountsaccount_documentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAccountsAccountDocumentGet(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);
        partial void OnGetAccountsAccountDocumentByAccountDocumentId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> GetAccountsAccountDocumentByAccountDocumentId(string accountdocumentid)
        {
            var items = Context.Accountsaccount_documents
                              .AsNoTracking()
                              .Where(i => i.account_document_id == accountdocumentid);

            items = items.Include(i => i.account);
            items = items.Include(i => i.document);
 
            OnGetAccountsAccountDocumentByAccountDocumentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAccountsAccountDocumentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAccountsAccountDocumentCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);
        partial void OnAfterAccountsAccountDocumentCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> CreateAccountsAccountDocument(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument accountsaccountdocument)
        {
            OnAccountsAccountDocumentCreated(accountsaccountdocument);

            var existingItem = Context.Accountsaccount_documents
                              .Where(i => i.account_document_id == accountsaccountdocument.account_document_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Accountsaccount_documents.Add(accountsaccountdocument);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(accountsaccountdocument).State = EntityState.Detached;
                throw;
            }

            OnAfterAccountsAccountDocumentCreated(accountsaccountdocument);

            return accountsaccountdocument;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> CancelAccountsAccountDocumentChanges(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAccountsAccountDocumentUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);
        partial void OnAfterAccountsAccountDocumentUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> UpdateAccountsAccountDocument(string accountdocumentid, EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument accountsaccountdocument)
        {
            OnAccountsAccountDocumentUpdated(accountsaccountdocument);

            var itemToUpdate = Context.Accountsaccount_documents
                              .Where(i => i.account_document_id == accountsaccountdocument.account_document_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(accountsaccountdocument);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAccountsAccountDocumentUpdated(accountsaccountdocument);

            return accountsaccountdocument;
        }

        partial void OnAccountsAccountDocumentDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);
        partial void OnAfterAccountsAccountDocumentDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> DeleteAccountsAccountDocument(string accountdocumentid)
        {
            var itemToDelete = Context.Accountsaccount_documents
                              .Where(i => i.account_document_id == accountdocumentid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAccountsAccountDocumentDeleted(itemToDelete);


            Context.Accountsaccount_documents.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAccountsAccountDocumentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAddressaddress_citiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAddressaddress_citiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAddressaddress_citiesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>> GetAddressaddress_cities(Query query = null)
        {
            var items = Context.Addressaddress_cities.AsQueryable();

            items = items.Include(i => i.address_state);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAddressaddress_citiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAddressAddressCityGet(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);
        partial void OnGetAddressAddressCityByCityId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> GetAddressAddressCityByCityId(string cityid)
        {
            var items = Context.Addressaddress_cities
                              .AsNoTracking()
                              .Where(i => i.city_id == cityid);

            items = items.Include(i => i.address_state);
 
            OnGetAddressAddressCityByCityId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAddressAddressCityGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAddressAddressCityCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);
        partial void OnAfterAddressAddressCityCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> CreateAddressAddressCity(EspoNew.Server.Models.EspoDbNew.AddressAddressCity addressaddresscity)
        {
            OnAddressAddressCityCreated(addressaddresscity);

            var existingItem = Context.Addressaddress_cities
                              .Where(i => i.city_id == addressaddresscity.city_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Addressaddress_cities.Add(addressaddresscity);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(addressaddresscity).State = EntityState.Detached;
                throw;
            }

            OnAfterAddressAddressCityCreated(addressaddresscity);

            return addressaddresscity;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> CancelAddressAddressCityChanges(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAddressAddressCityUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);
        partial void OnAfterAddressAddressCityUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> UpdateAddressAddressCity(string cityid, EspoNew.Server.Models.EspoDbNew.AddressAddressCity addressaddresscity)
        {
            OnAddressAddressCityUpdated(addressaddresscity);

            var itemToUpdate = Context.Addressaddress_cities
                              .Where(i => i.city_id == addressaddresscity.city_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(addressaddresscity);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAddressAddressCityUpdated(addressaddresscity);

            return addressaddresscity;
        }

        partial void OnAddressAddressCityDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);
        partial void OnAfterAddressAddressCityDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> DeleteAddressAddressCity(string cityid)
        {
            var itemToDelete = Context.Addressaddress_cities
                              .Where(i => i.city_id == cityid)
                              .Include(i => i.Contactscontacts)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAddressAddressCityDeleted(itemToDelete);


            Context.Addressaddress_cities.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAddressAddressCityDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAddressaddress_countriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAddressaddress_countriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAddressaddress_countriesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>> GetAddressaddress_countries(Query query = null)
        {
            var items = Context.Addressaddress_countries.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAddressaddress_countriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAddressAddressCountryGet(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);
        partial void OnGetAddressAddressCountryByCountryId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> GetAddressAddressCountryByCountryId(string countryid)
        {
            var items = Context.Addressaddress_countries
                              .AsNoTracking()
                              .Where(i => i.country_id == countryid);

 
            OnGetAddressAddressCountryByCountryId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAddressAddressCountryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAddressAddressCountryCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);
        partial void OnAfterAddressAddressCountryCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> CreateAddressAddressCountry(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressaddresscountry)
        {
            OnAddressAddressCountryCreated(addressaddresscountry);

            var existingItem = Context.Addressaddress_countries
                              .Where(i => i.country_id == addressaddresscountry.country_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Addressaddress_countries.Add(addressaddresscountry);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(addressaddresscountry).State = EntityState.Detached;
                throw;
            }

            OnAfterAddressAddressCountryCreated(addressaddresscountry);

            return addressaddresscountry;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> CancelAddressAddressCountryChanges(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAddressAddressCountryUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);
        partial void OnAfterAddressAddressCountryUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> UpdateAddressAddressCountry(string countryid, EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressaddresscountry)
        {
            OnAddressAddressCountryUpdated(addressaddresscountry);

            var itemToUpdate = Context.Addressaddress_countries
                              .Where(i => i.country_id == addressaddresscountry.country_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(addressaddresscountry);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAddressAddressCountryUpdated(addressaddresscountry);

            return addressaddresscountry;
        }

        partial void OnAddressAddressCountryDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);
        partial void OnAfterAddressAddressCountryDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> DeleteAddressAddressCountry(string countryid)
        {
            var itemToDelete = Context.Addressaddress_countries
                              .Where(i => i.country_id == countryid)
                              .Include(i => i.Addressaddress_states)
                              .Include(i => i.Contactscontacts)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAddressAddressCountryDeleted(itemToDelete);


            Context.Addressaddress_countries.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAddressAddressCountryDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAddressaddress_statesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_states/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_states/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAddressaddress_statesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_states/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_states/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAddressaddress_statesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressState> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressState>> GetAddressaddress_states(Query query = null)
        {
            var items = Context.Addressaddress_states.AsQueryable();

            items = items.Include(i => i.address_country);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAddressaddress_statesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAddressAddressStateGet(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);
        partial void OnGetAddressAddressStateByStateId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressState> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressState> GetAddressAddressStateByStateId(string stateid)
        {
            var items = Context.Addressaddress_states
                              .AsNoTracking()
                              .Where(i => i.state_id == stateid);

            items = items.Include(i => i.address_country);
 
            OnGetAddressAddressStateByStateId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAddressAddressStateGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAddressAddressStateCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);
        partial void OnAfterAddressAddressStateCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressState> CreateAddressAddressState(EspoNew.Server.Models.EspoDbNew.AddressAddressState addressaddressstate)
        {
            OnAddressAddressStateCreated(addressaddressstate);

            var existingItem = Context.Addressaddress_states
                              .Where(i => i.state_id == addressaddressstate.state_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Addressaddress_states.Add(addressaddressstate);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(addressaddressstate).State = EntityState.Detached;
                throw;
            }

            OnAfterAddressAddressStateCreated(addressaddressstate);

            return addressaddressstate;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressState> CancelAddressAddressStateChanges(EspoNew.Server.Models.EspoDbNew.AddressAddressState item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAddressAddressStateUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);
        partial void OnAfterAddressAddressStateUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressState> UpdateAddressAddressState(string stateid, EspoNew.Server.Models.EspoDbNew.AddressAddressState addressaddressstate)
        {
            OnAddressAddressStateUpdated(addressaddressstate);

            var itemToUpdate = Context.Addressaddress_states
                              .Where(i => i.state_id == addressaddressstate.state_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(addressaddressstate);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAddressAddressStateUpdated(addressaddressstate);

            return addressaddressstate;
        }

        partial void OnAddressAddressStateDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);
        partial void OnAfterAddressAddressStateDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressState> DeleteAddressAddressState(string stateid)
        {
            var itemToDelete = Context.Addressaddress_states
                              .Where(i => i.state_id == stateid)
                              .Include(i => i.Addressaddress_cities)
                              .Include(i => i.Contactscontacts)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAddressAddressStateDeleted(itemToDelete);


            Context.Addressaddress_states.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAddressAddressStateDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCallscallsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscalls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscalls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCallscallsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscalls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscalls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCallscallsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCall> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCall>> GetCallscalls(Query query = null)
        {
            var items = Context.Callscalls.AsQueryable();

            items = items.Include(i => i.account);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCallscallsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCallsCallGet(EspoNew.Server.Models.EspoDbNew.CallsCall item);
        partial void OnGetCallsCallByCallId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCall> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCall> GetCallsCallByCallId(string callid)
        {
            var items = Context.Callscalls
                              .AsNoTracking()
                              .Where(i => i.call_id == callid);

            items = items.Include(i => i.account);
 
            OnGetCallsCallByCallId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCallsCallGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCallsCallCreated(EspoNew.Server.Models.EspoDbNew.CallsCall item);
        partial void OnAfterCallsCallCreated(EspoNew.Server.Models.EspoDbNew.CallsCall item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCall> CreateCallsCall(EspoNew.Server.Models.EspoDbNew.CallsCall callscall)
        {
            OnCallsCallCreated(callscall);

            var existingItem = Context.Callscalls
                              .Where(i => i.call_id == callscall.call_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Callscalls.Add(callscall);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(callscall).State = EntityState.Detached;
                throw;
            }

            OnAfterCallsCallCreated(callscall);

            return callscall;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCall> CancelCallsCallChanges(EspoNew.Server.Models.EspoDbNew.CallsCall item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCallsCallUpdated(EspoNew.Server.Models.EspoDbNew.CallsCall item);
        partial void OnAfterCallsCallUpdated(EspoNew.Server.Models.EspoDbNew.CallsCall item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCall> UpdateCallsCall(string callid, EspoNew.Server.Models.EspoDbNew.CallsCall callscall)
        {
            OnCallsCallUpdated(callscall);

            var itemToUpdate = Context.Callscalls
                              .Where(i => i.call_id == callscall.call_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(callscall);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCallsCallUpdated(callscall);

            return callscall;
        }

        partial void OnCallsCallDeleted(EspoNew.Server.Models.EspoDbNew.CallsCall item);
        partial void OnAfterCallsCallDeleted(EspoNew.Server.Models.EspoDbNew.CallsCall item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCall> DeleteCallsCall(string callid)
        {
            var itemToDelete = Context.Callscalls
                              .Where(i => i.call_id == callid)
                              .Include(i => i.Callscall_contacts)
                              .Include(i => i.Callscall_leads)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCallsCallDeleted(itemToDelete);


            Context.Callscalls.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCallsCallDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCallscall_contactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscall_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscall_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCallscall_contactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscall_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscall_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCallscall_contactsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallContact> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallContact>> GetCallscall_contacts(Query query = null)
        {
            var items = Context.Callscall_contacts.AsQueryable();

            items = items.Include(i => i.call);
            items = items.Include(i => i.contact);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCallscall_contactsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCallsCallContactGet(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);
        partial void OnGetCallsCallContactByCallContactId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallContact> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallContact> GetCallsCallContactByCallContactId(string callcontactid)
        {
            var items = Context.Callscall_contacts
                              .AsNoTracking()
                              .Where(i => i.call_contact_id == callcontactid);

            items = items.Include(i => i.call);
            items = items.Include(i => i.contact);
 
            OnGetCallsCallContactByCallContactId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCallsCallContactGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCallsCallContactCreated(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);
        partial void OnAfterCallsCallContactCreated(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallContact> CreateCallsCallContact(EspoNew.Server.Models.EspoDbNew.CallsCallContact callscallcontact)
        {
            OnCallsCallContactCreated(callscallcontact);

            var existingItem = Context.Callscall_contacts
                              .Where(i => i.call_contact_id == callscallcontact.call_contact_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Callscall_contacts.Add(callscallcontact);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(callscallcontact).State = EntityState.Detached;
                throw;
            }

            OnAfterCallsCallContactCreated(callscallcontact);

            return callscallcontact;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallContact> CancelCallsCallContactChanges(EspoNew.Server.Models.EspoDbNew.CallsCallContact item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCallsCallContactUpdated(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);
        partial void OnAfterCallsCallContactUpdated(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallContact> UpdateCallsCallContact(string callcontactid, EspoNew.Server.Models.EspoDbNew.CallsCallContact callscallcontact)
        {
            OnCallsCallContactUpdated(callscallcontact);

            var itemToUpdate = Context.Callscall_contacts
                              .Where(i => i.call_contact_id == callscallcontact.call_contact_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(callscallcontact);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCallsCallContactUpdated(callscallcontact);

            return callscallcontact;
        }

        partial void OnCallsCallContactDeleted(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);
        partial void OnAfterCallsCallContactDeleted(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallContact> DeleteCallsCallContact(string callcontactid)
        {
            var itemToDelete = Context.Callscall_contacts
                              .Where(i => i.call_contact_id == callcontactid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCallsCallContactDeleted(itemToDelete);


            Context.Callscall_contacts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCallsCallContactDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCallscall_leadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscall_leads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscall_leads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCallscall_leadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscall_leads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscall_leads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCallscall_leadsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallLead> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallLead>> GetCallscall_leads(Query query = null)
        {
            var items = Context.Callscall_leads.AsQueryable();

            items = items.Include(i => i.call);
            items = items.Include(i => i.lead);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCallscall_leadsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCallsCallLeadGet(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);
        partial void OnGetCallsCallLeadByCallLeadId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallLead> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallLead> GetCallsCallLeadByCallLeadId(string callleadid)
        {
            var items = Context.Callscall_leads
                              .AsNoTracking()
                              .Where(i => i.call_lead_id == callleadid);

            items = items.Include(i => i.call);
            items = items.Include(i => i.lead);
 
            OnGetCallsCallLeadByCallLeadId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCallsCallLeadGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCallsCallLeadCreated(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);
        partial void OnAfterCallsCallLeadCreated(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallLead> CreateCallsCallLead(EspoNew.Server.Models.EspoDbNew.CallsCallLead callscalllead)
        {
            OnCallsCallLeadCreated(callscalllead);

            var existingItem = Context.Callscall_leads
                              .Where(i => i.call_lead_id == callscalllead.call_lead_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Callscall_leads.Add(callscalllead);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(callscalllead).State = EntityState.Detached;
                throw;
            }

            OnAfterCallsCallLeadCreated(callscalllead);

            return callscalllead;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallLead> CancelCallsCallLeadChanges(EspoNew.Server.Models.EspoDbNew.CallsCallLead item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCallsCallLeadUpdated(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);
        partial void OnAfterCallsCallLeadUpdated(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallLead> UpdateCallsCallLead(string callleadid, EspoNew.Server.Models.EspoDbNew.CallsCallLead callscalllead)
        {
            OnCallsCallLeadUpdated(callscalllead);

            var itemToUpdate = Context.Callscall_leads
                              .Where(i => i.call_lead_id == callscalllead.call_lead_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(callscalllead);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCallsCallLeadUpdated(callscalllead);

            return callscalllead;
        }

        partial void OnCallsCallLeadDeleted(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);
        partial void OnAfterCallsCallLeadDeleted(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallLead> DeleteCallsCallLead(string callleadid)
        {
            var itemToDelete = Context.Callscall_leads
                              .Where(i => i.call_lead_id == callleadid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCallsCallLeadDeleted(itemToDelete);


            Context.Callscall_leads.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCallsCallLeadDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCampaigncampaignsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/campaigncampaigns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/campaigncampaigns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCampaigncampaignsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/campaigncampaigns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/campaigncampaigns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCampaigncampaignsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>> GetCampaigncampaigns(Query query = null)
        {
            var items = Context.Campaigncampaigns.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCampaigncampaignsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCampaignCampaignGet(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);
        partial void OnGetCampaignCampaignByCampaignId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> GetCampaignCampaignByCampaignId(string campaignid)
        {
            var items = Context.Campaigncampaigns
                              .AsNoTracking()
                              .Where(i => i.campaign_id == campaignid);

 
            OnGetCampaignCampaignByCampaignId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCampaignCampaignGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCampaignCampaignCreated(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);
        partial void OnAfterCampaignCampaignCreated(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> CreateCampaignCampaign(EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaign)
        {
            OnCampaignCampaignCreated(campaigncampaign);

            var existingItem = Context.Campaigncampaigns
                              .Where(i => i.campaign_id == campaigncampaign.campaign_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Campaigncampaigns.Add(campaigncampaign);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(campaigncampaign).State = EntityState.Detached;
                throw;
            }

            OnAfterCampaignCampaignCreated(campaigncampaign);

            return campaigncampaign;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> CancelCampaignCampaignChanges(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCampaignCampaignUpdated(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);
        partial void OnAfterCampaignCampaignUpdated(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> UpdateCampaignCampaign(string campaignid, EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaign)
        {
            OnCampaignCampaignUpdated(campaigncampaign);

            var itemToUpdate = Context.Campaigncampaigns
                              .Where(i => i.campaign_id == campaigncampaign.campaign_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(campaigncampaign);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCampaignCampaignUpdated(campaigncampaign);

            return campaigncampaign;
        }

        partial void OnCampaignCampaignDeleted(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);
        partial void OnAfterCampaignCampaignDeleted(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> DeleteCampaignCampaign(string campaignid)
        {
            var itemToDelete = Context.Campaigncampaigns
                              .Where(i => i.campaign_id == campaignid)
                              .Include(i => i.Accountsaccounts)
                              .Include(i => i.Contactscontacts)
                              .Include(i => i.Leadsleads)
                              .Include(i => i.Opportunitiesopportunities)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCampaignCampaignDeleted(itemToDelete);


            Context.Campaigncampaigns.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCampaignCampaignDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCases_casesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/cases_cases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/cases_cases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCases_casesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/cases_cases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/cases_cases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCases_casesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CasesCase> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.CasesCase>> GetCases_cases(Query query = null)
        {
            var items = Context.Cases_cases.AsQueryable();

            items = items.Include(i => i.account);
            items = items.Include(i => i.contact);
            items = items.Include(i => i.lead);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCases_casesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCasesCaseGet(EspoNew.Server.Models.EspoDbNew.CasesCase item);
        partial void OnGetCasesCaseByCaseId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CasesCase> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCase> GetCasesCaseByCaseId(string caseid)
        {
            var items = Context.Cases_cases
                              .AsNoTracking()
                              .Where(i => i.case_id == caseid);

            items = items.Include(i => i.account);
            items = items.Include(i => i.contact);
            items = items.Include(i => i.lead);
 
            OnGetCasesCaseByCaseId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCasesCaseGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCasesCaseCreated(EspoNew.Server.Models.EspoDbNew.CasesCase item);
        partial void OnAfterCasesCaseCreated(EspoNew.Server.Models.EspoDbNew.CasesCase item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCase> CreateCasesCase(EspoNew.Server.Models.EspoDbNew.CasesCase casescase)
        {
            OnCasesCaseCreated(casescase);

            var existingItem = Context.Cases_cases
                              .Where(i => i.case_id == casescase.case_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Cases_cases.Add(casescase);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(casescase).State = EntityState.Detached;
                throw;
            }

            OnAfterCasesCaseCreated(casescase);

            return casescase;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCase> CancelCasesCaseChanges(EspoNew.Server.Models.EspoDbNew.CasesCase item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCasesCaseUpdated(EspoNew.Server.Models.EspoDbNew.CasesCase item);
        partial void OnAfterCasesCaseUpdated(EspoNew.Server.Models.EspoDbNew.CasesCase item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCase> UpdateCasesCase(string caseid, EspoNew.Server.Models.EspoDbNew.CasesCase casescase)
        {
            OnCasesCaseUpdated(casescase);

            var itemToUpdate = Context.Cases_cases
                              .Where(i => i.case_id == casescase.case_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(casescase);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCasesCaseUpdated(casescase);

            return casescase;
        }

        partial void OnCasesCaseDeleted(EspoNew.Server.Models.EspoDbNew.CasesCase item);
        partial void OnAfterCasesCaseDeleted(EspoNew.Server.Models.EspoDbNew.CasesCase item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCase> DeleteCasesCase(string caseid)
        {
            var itemToDelete = Context.Cases_cases
                              .Where(i => i.case_id == caseid)
                              .Include(i => i.Casescase_contacts)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCasesCaseDeleted(itemToDelete);


            Context.Cases_cases.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCasesCaseDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCasescase_contactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/casescase_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/casescase_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCasescase_contactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/casescase_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/casescase_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCasescase_contactsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>> GetCasescase_contacts(Query query = null)
        {
            var items = Context.Casescase_contacts.AsQueryable();

            items = items.Include(i => i._case);
            items = items.Include(i => i.contact);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCasescase_contactsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCasesCaseContactGet(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);
        partial void OnGetCasesCaseContactByCaseContactId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> GetCasesCaseContactByCaseContactId(string casecontactid)
        {
            var items = Context.Casescase_contacts
                              .AsNoTracking()
                              .Where(i => i.case_contact_id == casecontactid);

            items = items.Include(i => i._case);
            items = items.Include(i => i.contact);
 
            OnGetCasesCaseContactByCaseContactId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCasesCaseContactGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCasesCaseContactCreated(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);
        partial void OnAfterCasesCaseContactCreated(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> CreateCasesCaseContact(EspoNew.Server.Models.EspoDbNew.CasesCaseContact casescasecontact)
        {
            OnCasesCaseContactCreated(casescasecontact);

            var existingItem = Context.Casescase_contacts
                              .Where(i => i.case_contact_id == casescasecontact.case_contact_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Casescase_contacts.Add(casescasecontact);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(casescasecontact).State = EntityState.Detached;
                throw;
            }

            OnAfterCasesCaseContactCreated(casescasecontact);

            return casescasecontact;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> CancelCasesCaseContactChanges(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCasesCaseContactUpdated(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);
        partial void OnAfterCasesCaseContactUpdated(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> UpdateCasesCaseContact(string casecontactid, EspoNew.Server.Models.EspoDbNew.CasesCaseContact casescasecontact)
        {
            OnCasesCaseContactUpdated(casescasecontact);

            var itemToUpdate = Context.Casescase_contacts
                              .Where(i => i.case_contact_id == casescasecontact.case_contact_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(casescasecontact);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCasesCaseContactUpdated(casescasecontact);

            return casescasecontact;
        }

        partial void OnCasesCaseContactDeleted(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);
        partial void OnAfterCasesCaseContactDeleted(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> DeleteCasesCaseContact(string casecontactid)
        {
            var itemToDelete = Context.Casescase_contacts
                              .Where(i => i.case_contact_id == casecontactid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCasesCaseContactDeleted(itemToDelete);


            Context.Casescase_contacts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCasesCaseContactDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportContactscontactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContactscontactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContactscontactsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContact> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContact>> GetContactscontacts(Query query = null)
        {
            var items = Context.Contactscontacts.AsQueryable();

            items = items.Include(i => i.account);
            items = items.Include(i => i.address_city);
            items = items.Include(i => i.address_country);
            items = items.Include(i => i.address_state);
            items = items.Include(i => i.campaign);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnContactscontactsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContactsContactGet(EspoNew.Server.Models.EspoDbNew.ContactsContact item);
        partial void OnGetContactsContactByContactId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContact> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContact> GetContactsContactByContactId(string contactid)
        {
            var items = Context.Contactscontacts
                              .AsNoTracking()
                              .Where(i => i.contact_id == contactid);

            items = items.Include(i => i.account);
            items = items.Include(i => i.address_city);
            items = items.Include(i => i.address_country);
            items = items.Include(i => i.address_state);
            items = items.Include(i => i.campaign);
 
            OnGetContactsContactByContactId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnContactsContactGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnContactsContactCreated(EspoNew.Server.Models.EspoDbNew.ContactsContact item);
        partial void OnAfterContactsContactCreated(EspoNew.Server.Models.EspoDbNew.ContactsContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContact> CreateContactsContact(EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontact)
        {
            OnContactsContactCreated(contactscontact);

            var existingItem = Context.Contactscontacts
                              .Where(i => i.contact_id == contactscontact.contact_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Contactscontacts.Add(contactscontact);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(contactscontact).State = EntityState.Detached;
                throw;
            }

            OnAfterContactsContactCreated(contactscontact);

            return contactscontact;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContact> CancelContactsContactChanges(EspoNew.Server.Models.EspoDbNew.ContactsContact item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnContactsContactUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContact item);
        partial void OnAfterContactsContactUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContact> UpdateContactsContact(string contactid, EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontact)
        {
            OnContactsContactUpdated(contactscontact);

            var itemToUpdate = Context.Contactscontacts
                              .Where(i => i.contact_id == contactscontact.contact_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contactscontact);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterContactsContactUpdated(contactscontact);

            return contactscontact;
        }

        partial void OnContactsContactDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContact item);
        partial void OnAfterContactsContactDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContact item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContact> DeleteContactsContact(string contactid)
        {
            var itemToDelete = Context.Contactscontacts
                              .Where(i => i.contact_id == contactid)
                              .Include(i => i.Accountsaccount_contacts)
                              .Include(i => i.Callscall_contacts)
                              .Include(i => i.Cases_cases)
                              .Include(i => i.Casescase_contacts)
                              .Include(i => i.Contactscontact_documents)
                              .Include(i => i.Contactscontact_meetings)
                              .Include(i => i.Contactscontact_opportunities)
                              .Include(i => i.Employeesemployees)
                              .Include(i => i.Opportunitiesopportunities)
                              .Include(i => i.Taskstasks)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContactsContactDeleted(itemToDelete);


            Context.Contactscontacts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContactsContactDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportContactscontact_documentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContactscontact_documentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContactscontact_documentsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>> GetContactscontact_documents(Query query = null)
        {
            var items = Context.Contactscontact_documents.AsQueryable();

            items = items.Include(i => i.contact);
            items = items.Include(i => i.document);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnContactscontact_documentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContactsContactDocumentGet(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);
        partial void OnGetContactsContactDocumentByContactDocumentId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> GetContactsContactDocumentByContactDocumentId(string contactdocumentid)
        {
            var items = Context.Contactscontact_documents
                              .AsNoTracking()
                              .Where(i => i.contact_document_id == contactdocumentid);

            items = items.Include(i => i.contact);
            items = items.Include(i => i.document);
 
            OnGetContactsContactDocumentByContactDocumentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnContactsContactDocumentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnContactsContactDocumentCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);
        partial void OnAfterContactsContactDocumentCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> CreateContactsContactDocument(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument contactscontactdocument)
        {
            OnContactsContactDocumentCreated(contactscontactdocument);

            var existingItem = Context.Contactscontact_documents
                              .Where(i => i.contact_document_id == contactscontactdocument.contact_document_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Contactscontact_documents.Add(contactscontactdocument);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(contactscontactdocument).State = EntityState.Detached;
                throw;
            }

            OnAfterContactsContactDocumentCreated(contactscontactdocument);

            return contactscontactdocument;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> CancelContactsContactDocumentChanges(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnContactsContactDocumentUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);
        partial void OnAfterContactsContactDocumentUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> UpdateContactsContactDocument(string contactdocumentid, EspoNew.Server.Models.EspoDbNew.ContactsContactDocument contactscontactdocument)
        {
            OnContactsContactDocumentUpdated(contactscontactdocument);

            var itemToUpdate = Context.Contactscontact_documents
                              .Where(i => i.contact_document_id == contactscontactdocument.contact_document_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contactscontactdocument);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterContactsContactDocumentUpdated(contactscontactdocument);

            return contactscontactdocument;
        }

        partial void OnContactsContactDocumentDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);
        partial void OnAfterContactsContactDocumentDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> DeleteContactsContactDocument(string contactdocumentid)
        {
            var itemToDelete = Context.Contactscontact_documents
                              .Where(i => i.contact_document_id == contactdocumentid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContactsContactDocumentDeleted(itemToDelete);


            Context.Contactscontact_documents.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContactsContactDocumentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportContactscontact_meetingsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_meetings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_meetings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContactscontact_meetingsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_meetings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_meetings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContactscontact_meetingsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>> GetContactscontact_meetings(Query query = null)
        {
            var items = Context.Contactscontact_meetings.AsQueryable();

            items = items.Include(i => i.contact);
            items = items.Include(i => i.meeting);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnContactscontact_meetingsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContactsContactMeetingGet(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);
        partial void OnGetContactsContactMeetingByContactMeetingId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> GetContactsContactMeetingByContactMeetingId(string contactmeetingid)
        {
            var items = Context.Contactscontact_meetings
                              .AsNoTracking()
                              .Where(i => i.contact_meeting_id == contactmeetingid);

            items = items.Include(i => i.contact);
            items = items.Include(i => i.meeting);
 
            OnGetContactsContactMeetingByContactMeetingId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnContactsContactMeetingGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnContactsContactMeetingCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);
        partial void OnAfterContactsContactMeetingCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> CreateContactsContactMeeting(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting contactscontactmeeting)
        {
            OnContactsContactMeetingCreated(contactscontactmeeting);

            var existingItem = Context.Contactscontact_meetings
                              .Where(i => i.contact_meeting_id == contactscontactmeeting.contact_meeting_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Contactscontact_meetings.Add(contactscontactmeeting);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(contactscontactmeeting).State = EntityState.Detached;
                throw;
            }

            OnAfterContactsContactMeetingCreated(contactscontactmeeting);

            return contactscontactmeeting;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> CancelContactsContactMeetingChanges(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnContactsContactMeetingUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);
        partial void OnAfterContactsContactMeetingUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> UpdateContactsContactMeeting(string contactmeetingid, EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting contactscontactmeeting)
        {
            OnContactsContactMeetingUpdated(contactscontactmeeting);

            var itemToUpdate = Context.Contactscontact_meetings
                              .Where(i => i.contact_meeting_id == contactscontactmeeting.contact_meeting_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contactscontactmeeting);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterContactsContactMeetingUpdated(contactscontactmeeting);

            return contactscontactmeeting;
        }

        partial void OnContactsContactMeetingDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);
        partial void OnAfterContactsContactMeetingDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> DeleteContactsContactMeeting(string contactmeetingid)
        {
            var itemToDelete = Context.Contactscontact_meetings
                              .Where(i => i.contact_meeting_id == contactmeetingid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContactsContactMeetingDeleted(itemToDelete);


            Context.Contactscontact_meetings.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContactsContactMeetingDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportContactscontact_opportunitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContactscontact_opportunitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContactscontact_opportunitiesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>> GetContactscontact_opportunities(Query query = null)
        {
            var items = Context.Contactscontact_opportunities.AsQueryable();

            items = items.Include(i => i.contact);
            items = items.Include(i => i.opportunity);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnContactscontact_opportunitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContactsContactOpportunityGet(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);
        partial void OnGetContactsContactOpportunityByContactOpportunityId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> GetContactsContactOpportunityByContactOpportunityId(string contactopportunityid)
        {
            var items = Context.Contactscontact_opportunities
                              .AsNoTracking()
                              .Where(i => i.contact_opportunity_id == contactopportunityid);

            items = items.Include(i => i.contact);
            items = items.Include(i => i.opportunity);
 
            OnGetContactsContactOpportunityByContactOpportunityId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnContactsContactOpportunityGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnContactsContactOpportunityCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);
        partial void OnAfterContactsContactOpportunityCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> CreateContactsContactOpportunity(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity contactscontactopportunity)
        {
            OnContactsContactOpportunityCreated(contactscontactopportunity);

            var existingItem = Context.Contactscontact_opportunities
                              .Where(i => i.contact_opportunity_id == contactscontactopportunity.contact_opportunity_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Contactscontact_opportunities.Add(contactscontactopportunity);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(contactscontactopportunity).State = EntityState.Detached;
                throw;
            }

            OnAfterContactsContactOpportunityCreated(contactscontactopportunity);

            return contactscontactopportunity;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> CancelContactsContactOpportunityChanges(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnContactsContactOpportunityUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);
        partial void OnAfterContactsContactOpportunityUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> UpdateContactsContactOpportunity(string contactopportunityid, EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity contactscontactopportunity)
        {
            OnContactsContactOpportunityUpdated(contactscontactopportunity);

            var itemToUpdate = Context.Contactscontact_opportunities
                              .Where(i => i.contact_opportunity_id == contactscontactopportunity.contact_opportunity_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contactscontactopportunity);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterContactsContactOpportunityUpdated(contactscontactopportunity);

            return contactscontactopportunity;
        }

        partial void OnContactsContactOpportunityDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);
        partial void OnAfterContactsContactOpportunityDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> DeleteContactsContactOpportunity(string contactopportunityid)
        {
            var itemToDelete = Context.Contactscontact_opportunities
                              .Where(i => i.contact_opportunity_id == contactopportunityid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContactsContactOpportunityDeleted(itemToDelete);


            Context.Contactscontact_opportunities.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContactsContactOpportunityDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDocumentsdocumentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/documentsdocuments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/documentsdocuments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDocumentsdocumentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/documentsdocuments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/documentsdocuments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDocumentsdocumentsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>> GetDocumentsdocuments(Query query = null)
        {
            var items = Context.Documentsdocuments.AsQueryable();

            items = items.Include(i => i.employee);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDocumentsdocumentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDocumentsDocumentGet(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);
        partial void OnGetDocumentsDocumentByDocumentId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> GetDocumentsDocumentByDocumentId(string documentid)
        {
            var items = Context.Documentsdocuments
                              .AsNoTracking()
                              .Where(i => i.document_id == documentid);

            items = items.Include(i => i.employee);
 
            OnGetDocumentsDocumentByDocumentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDocumentsDocumentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDocumentsDocumentCreated(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);
        partial void OnAfterDocumentsDocumentCreated(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> CreateDocumentsDocument(EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocument)
        {
            OnDocumentsDocumentCreated(documentsdocument);

            var existingItem = Context.Documentsdocuments
                              .Where(i => i.document_id == documentsdocument.document_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Documentsdocuments.Add(documentsdocument);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(documentsdocument).State = EntityState.Detached;
                throw;
            }

            OnAfterDocumentsDocumentCreated(documentsdocument);

            return documentsdocument;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> CancelDocumentsDocumentChanges(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDocumentsDocumentUpdated(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);
        partial void OnAfterDocumentsDocumentUpdated(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> UpdateDocumentsDocument(string documentid, EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocument)
        {
            OnDocumentsDocumentUpdated(documentsdocument);

            var itemToUpdate = Context.Documentsdocuments
                              .Where(i => i.document_id == documentsdocument.document_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(documentsdocument);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDocumentsDocumentUpdated(documentsdocument);

            return documentsdocument;
        }

        partial void OnDocumentsDocumentDeleted(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);
        partial void OnAfterDocumentsDocumentDeleted(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> DeleteDocumentsDocument(string documentid)
        {
            var itemToDelete = Context.Documentsdocuments
                              .Where(i => i.document_id == documentid)
                              .Include(i => i.Accountsaccount_documents)
                              .Include(i => i.Contactscontact_documents)
                              .Include(i => i.Documentsdocument_leads)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDocumentsDocumentDeleted(itemToDelete);


            Context.Documentsdocuments.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDocumentsDocumentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDocumentsdocument_leadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/documentsdocument_leads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/documentsdocument_leads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDocumentsdocument_leadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/documentsdocument_leads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/documentsdocument_leads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDocumentsdocument_leadsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>> GetDocumentsdocument_leads(Query query = null)
        {
            var items = Context.Documentsdocument_leads.AsQueryable();

            items = items.Include(i => i.document);
            items = items.Include(i => i.lead);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDocumentsdocument_leadsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDocumentsDocumentLeadGet(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);
        partial void OnGetDocumentsDocumentLeadByDocumentLeadId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> GetDocumentsDocumentLeadByDocumentLeadId(string documentleadid)
        {
            var items = Context.Documentsdocument_leads
                              .AsNoTracking()
                              .Where(i => i.document_lead_id == documentleadid);

            items = items.Include(i => i.document);
            items = items.Include(i => i.lead);
 
            OnGetDocumentsDocumentLeadByDocumentLeadId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDocumentsDocumentLeadGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDocumentsDocumentLeadCreated(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);
        partial void OnAfterDocumentsDocumentLeadCreated(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> CreateDocumentsDocumentLead(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead documentsdocumentlead)
        {
            OnDocumentsDocumentLeadCreated(documentsdocumentlead);

            var existingItem = Context.Documentsdocument_leads
                              .Where(i => i.document_lead_id == documentsdocumentlead.document_lead_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Documentsdocument_leads.Add(documentsdocumentlead);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(documentsdocumentlead).State = EntityState.Detached;
                throw;
            }

            OnAfterDocumentsDocumentLeadCreated(documentsdocumentlead);

            return documentsdocumentlead;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> CancelDocumentsDocumentLeadChanges(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDocumentsDocumentLeadUpdated(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);
        partial void OnAfterDocumentsDocumentLeadUpdated(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> UpdateDocumentsDocumentLead(string documentleadid, EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead documentsdocumentlead)
        {
            OnDocumentsDocumentLeadUpdated(documentsdocumentlead);

            var itemToUpdate = Context.Documentsdocument_leads
                              .Where(i => i.document_lead_id == documentsdocumentlead.document_lead_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(documentsdocumentlead);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDocumentsDocumentLeadUpdated(documentsdocumentlead);

            return documentsdocumentlead;
        }

        partial void OnDocumentsDocumentLeadDeleted(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);
        partial void OnAfterDocumentsDocumentLeadDeleted(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> DeleteDocumentsDocumentLead(string documentleadid)
        {
            var itemToDelete = Context.Documentsdocument_leads
                              .Where(i => i.document_lead_id == documentleadid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDocumentsDocumentLeadDeleted(itemToDelete);


            Context.Documentsdocument_leads.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDocumentsDocumentLeadDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEmailemailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/emailemails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/emailemails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmailemailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/emailemails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/emailemails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmailemailsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmail> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmail>> GetEmailemails(Query query = null)
        {
            var items = Context.Emailemails.AsQueryable();

            items = items.Include(i => i.account);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEmailemailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmailEmailGet(EspoNew.Server.Models.EspoDbNew.EmailEmail item);
        partial void OnGetEmailEmailByEmailId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmail> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmail> GetEmailEmailByEmailId(string emailid)
        {
            var items = Context.Emailemails
                              .AsNoTracking()
                              .Where(i => i.email_id == emailid);

            items = items.Include(i => i.account);
 
            OnGetEmailEmailByEmailId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEmailEmailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEmailEmailCreated(EspoNew.Server.Models.EspoDbNew.EmailEmail item);
        partial void OnAfterEmailEmailCreated(EspoNew.Server.Models.EspoDbNew.EmailEmail item);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmail> CreateEmailEmail(EspoNew.Server.Models.EspoDbNew.EmailEmail emailemail)
        {
            OnEmailEmailCreated(emailemail);

            var existingItem = Context.Emailemails
                              .Where(i => i.email_id == emailemail.email_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Emailemails.Add(emailemail);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(emailemail).State = EntityState.Detached;
                throw;
            }

            OnAfterEmailEmailCreated(emailemail);

            return emailemail;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmail> CancelEmailEmailChanges(EspoNew.Server.Models.EspoDbNew.EmailEmail item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmailEmailUpdated(EspoNew.Server.Models.EspoDbNew.EmailEmail item);
        partial void OnAfterEmailEmailUpdated(EspoNew.Server.Models.EspoDbNew.EmailEmail item);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmail> UpdateEmailEmail(string emailid, EspoNew.Server.Models.EspoDbNew.EmailEmail emailemail)
        {
            OnEmailEmailUpdated(emailemail);

            var itemToUpdate = Context.Emailemails
                              .Where(i => i.email_id == emailemail.email_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(emailemail);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEmailEmailUpdated(emailemail);

            return emailemail;
        }

        partial void OnEmailEmailDeleted(EspoNew.Server.Models.EspoDbNew.EmailEmail item);
        partial void OnAfterEmailEmailDeleted(EspoNew.Server.Models.EspoDbNew.EmailEmail item);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmail> DeleteEmailEmail(string emailid)
        {
            var itemToDelete = Context.Emailemails
                              .Where(i => i.email_id == emailid)
                              .Include(i => i.Taskstasks)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmailEmailDeleted(itemToDelete);


            Context.Emailemails.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmailEmailDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEmailemail_accountsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/emailemail_accounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/emailemail_accounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmailemail_accountsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/emailemail_accounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/emailemail_accounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmailemail_accountsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>> GetEmailemail_accounts(Query query = null)
        {
            var items = Context.Emailemail_accounts.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEmailemail_accountsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmailEmailAccountGet(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);
        partial void OnGetEmailEmailAccountByEmailAccountId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> GetEmailEmailAccountByEmailAccountId(string emailaccountid)
        {
            var items = Context.Emailemail_accounts
                              .AsNoTracking()
                              .Where(i => i.email_account_id == emailaccountid);

 
            OnGetEmailEmailAccountByEmailAccountId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEmailEmailAccountGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEmailEmailAccountCreated(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);
        partial void OnAfterEmailEmailAccountCreated(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> CreateEmailEmailAccount(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount emailemailaccount)
        {
            OnEmailEmailAccountCreated(emailemailaccount);

            var existingItem = Context.Emailemail_accounts
                              .Where(i => i.email_account_id == emailemailaccount.email_account_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Emailemail_accounts.Add(emailemailaccount);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(emailemailaccount).State = EntityState.Detached;
                throw;
            }

            OnAfterEmailEmailAccountCreated(emailemailaccount);

            return emailemailaccount;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> CancelEmailEmailAccountChanges(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmailEmailAccountUpdated(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);
        partial void OnAfterEmailEmailAccountUpdated(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> UpdateEmailEmailAccount(string emailaccountid, EspoNew.Server.Models.EspoDbNew.EmailEmailAccount emailemailaccount)
        {
            OnEmailEmailAccountUpdated(emailemailaccount);

            var itemToUpdate = Context.Emailemail_accounts
                              .Where(i => i.email_account_id == emailemailaccount.email_account_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(emailemailaccount);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEmailEmailAccountUpdated(emailemailaccount);

            return emailemailaccount;
        }

        partial void OnEmailEmailAccountDeleted(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);
        partial void OnAfterEmailEmailAccountDeleted(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> DeleteEmailEmailAccount(string emailaccountid)
        {
            var itemToDelete = Context.Emailemail_accounts
                              .Where(i => i.email_account_id == emailaccountid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmailEmailAccountDeleted(itemToDelete);


            Context.Emailemail_accounts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmailEmailAccountDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEmployeesemployeesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/employeesemployees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/employeesemployees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmployeesemployeesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/employeesemployees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/employeesemployees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmployeesemployeesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>> GetEmployeesemployees(Query query = null)
        {
            var items = Context.Employeesemployees.AsQueryable();

            items = items.Include(i => i.contact);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEmployeesemployeesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmployeesEmployeeGet(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);
        partial void OnGetEmployeesEmployeeByEmployeeId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> GetEmployeesEmployeeByEmployeeId(string employeeid)
        {
            var items = Context.Employeesemployees
                              .AsNoTracking()
                              .Where(i => i.employee_id == employeeid);

            items = items.Include(i => i.contact);
 
            OnGetEmployeesEmployeeByEmployeeId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEmployeesEmployeeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEmployeesEmployeeCreated(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);
        partial void OnAfterEmployeesEmployeeCreated(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> CreateEmployeesEmployee(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesemployee)
        {
            OnEmployeesEmployeeCreated(employeesemployee);

            var existingItem = Context.Employeesemployees
                              .Where(i => i.employee_id == employeesemployee.employee_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Employeesemployees.Add(employeesemployee);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(employeesemployee).State = EntityState.Detached;
                throw;
            }

            OnAfterEmployeesEmployeeCreated(employeesemployee);

            return employeesemployee;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> CancelEmployeesEmployeeChanges(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmployeesEmployeeUpdated(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);
        partial void OnAfterEmployeesEmployeeUpdated(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> UpdateEmployeesEmployee(string employeeid, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesemployee)
        {
            OnEmployeesEmployeeUpdated(employeesemployee);

            var itemToUpdate = Context.Employeesemployees
                              .Where(i => i.employee_id == employeesemployee.employee_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(employeesemployee);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEmployeesEmployeeUpdated(employeesemployee);

            return employeesemployee;
        }

        partial void OnEmployeesEmployeeDeleted(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);
        partial void OnAfterEmployeesEmployeeDeleted(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> DeleteEmployeesEmployee(string employeeid)
        {
            var itemToDelete = Context.Employeesemployees
                              .Where(i => i.employee_id == employeeid)
                              .Include(i => i.Accountsaccounts)
                              .Include(i => i.Documentsdocuments)
                              .Include(i => i.Opportunitiesopportunities)
                              .Include(i => i.Taskstasks)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmployeesEmployeeDeleted(itemToDelete);


            Context.Employeesemployees.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmployeesEmployeeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportLeadsleadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/leadsleads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/leadsleads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLeadsleadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/leadsleads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/leadsleads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLeadsleadsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.LeadsLead> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.LeadsLead>> GetLeadsleads(Query query = null)
        {
            var items = Context.Leadsleads.AsQueryable();

            items = items.Include(i => i.campaign);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLeadsleadsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLeadsLeadGet(EspoNew.Server.Models.EspoDbNew.LeadsLead item);
        partial void OnGetLeadsLeadByLeadId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.LeadsLead> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.LeadsLead> GetLeadsLeadByLeadId(string leadid)
        {
            var items = Context.Leadsleads
                              .AsNoTracking()
                              .Where(i => i.lead_id == leadid);

            items = items.Include(i => i.campaign);
 
            OnGetLeadsLeadByLeadId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLeadsLeadGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLeadsLeadCreated(EspoNew.Server.Models.EspoDbNew.LeadsLead item);
        partial void OnAfterLeadsLeadCreated(EspoNew.Server.Models.EspoDbNew.LeadsLead item);

        public async Task<EspoNew.Server.Models.EspoDbNew.LeadsLead> CreateLeadsLead(EspoNew.Server.Models.EspoDbNew.LeadsLead leadslead)
        {
            OnLeadsLeadCreated(leadslead);

            var existingItem = Context.Leadsleads
                              .Where(i => i.lead_id == leadslead.lead_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Leadsleads.Add(leadslead);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(leadslead).State = EntityState.Detached;
                throw;
            }

            OnAfterLeadsLeadCreated(leadslead);

            return leadslead;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.LeadsLead> CancelLeadsLeadChanges(EspoNew.Server.Models.EspoDbNew.LeadsLead item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLeadsLeadUpdated(EspoNew.Server.Models.EspoDbNew.LeadsLead item);
        partial void OnAfterLeadsLeadUpdated(EspoNew.Server.Models.EspoDbNew.LeadsLead item);

        public async Task<EspoNew.Server.Models.EspoDbNew.LeadsLead> UpdateLeadsLead(string leadid, EspoNew.Server.Models.EspoDbNew.LeadsLead leadslead)
        {
            OnLeadsLeadUpdated(leadslead);

            var itemToUpdate = Context.Leadsleads
                              .Where(i => i.lead_id == leadslead.lead_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(leadslead);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLeadsLeadUpdated(leadslead);

            return leadslead;
        }

        partial void OnLeadsLeadDeleted(EspoNew.Server.Models.EspoDbNew.LeadsLead item);
        partial void OnAfterLeadsLeadDeleted(EspoNew.Server.Models.EspoDbNew.LeadsLead item);

        public async Task<EspoNew.Server.Models.EspoDbNew.LeadsLead> DeleteLeadsLead(string leadid)
        {
            var itemToDelete = Context.Leadsleads
                              .Where(i => i.lead_id == leadid)
                              .Include(i => i.Callscall_leads)
                              .Include(i => i.Cases_cases)
                              .Include(i => i.Documentsdocument_leads)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLeadsLeadDeleted(itemToDelete);


            Context.Leadsleads.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLeadsLeadDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMeetingsmeetingsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/meetingsmeetings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/meetingsmeetings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMeetingsmeetingsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/meetingsmeetings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/meetingsmeetings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMeetingsmeetingsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>> GetMeetingsmeetings(Query query = null)
        {
            var items = Context.Meetingsmeetings.AsQueryable();

            items = items.Include(i => i.account);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnMeetingsmeetingsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMeetingsMeetingGet(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);
        partial void OnGetMeetingsMeetingByMeetingId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> GetMeetingsMeetingByMeetingId(string meetingid)
        {
            var items = Context.Meetingsmeetings
                              .AsNoTracking()
                              .Where(i => i.meeting_id == meetingid);

            items = items.Include(i => i.account);
 
            OnGetMeetingsMeetingByMeetingId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMeetingsMeetingGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMeetingsMeetingCreated(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);
        partial void OnAfterMeetingsMeetingCreated(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);

        public async Task<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> CreateMeetingsMeeting(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsmeeting)
        {
            OnMeetingsMeetingCreated(meetingsmeeting);

            var existingItem = Context.Meetingsmeetings
                              .Where(i => i.meeting_id == meetingsmeeting.meeting_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Meetingsmeetings.Add(meetingsmeeting);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(meetingsmeeting).State = EntityState.Detached;
                throw;
            }

            OnAfterMeetingsMeetingCreated(meetingsmeeting);

            return meetingsmeeting;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> CancelMeetingsMeetingChanges(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMeetingsMeetingUpdated(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);
        partial void OnAfterMeetingsMeetingUpdated(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);

        public async Task<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> UpdateMeetingsMeeting(string meetingid, EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsmeeting)
        {
            OnMeetingsMeetingUpdated(meetingsmeeting);

            var itemToUpdate = Context.Meetingsmeetings
                              .Where(i => i.meeting_id == meetingsmeeting.meeting_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(meetingsmeeting);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMeetingsMeetingUpdated(meetingsmeeting);

            return meetingsmeeting;
        }

        partial void OnMeetingsMeetingDeleted(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);
        partial void OnAfterMeetingsMeetingDeleted(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);

        public async Task<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> DeleteMeetingsMeeting(string meetingid)
        {
            var itemToDelete = Context.Meetingsmeetings
                              .Where(i => i.meeting_id == meetingid)
                              .Include(i => i.Contactscontact_meetings)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMeetingsMeetingDeleted(itemToDelete);


            Context.Meetingsmeetings.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMeetingsMeetingDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportOpportunitiesopportunitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/opportunitiesopportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/opportunitiesopportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOpportunitiesopportunitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/opportunitiesopportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/opportunitiesopportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOpportunitiesopportunitiesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>> GetOpportunitiesopportunities(Query query = null)
        {
            var items = Context.Opportunitiesopportunities.AsQueryable();

            items = items.Include(i => i.account);
            items = items.Include(i => i.employee);
            items = items.Include(i => i.campaign);
            items = items.Include(i => i.contact);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnOpportunitiesopportunitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOpportunitiesOpportunityGet(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);
        partial void OnGetOpportunitiesOpportunityByOpportunityId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> GetOpportunitiesOpportunityByOpportunityId(string opportunityid)
        {
            var items = Context.Opportunitiesopportunities
                              .AsNoTracking()
                              .Where(i => i.opportunity_id == opportunityid);

            items = items.Include(i => i.account);
            items = items.Include(i => i.employee);
            items = items.Include(i => i.campaign);
            items = items.Include(i => i.contact);
 
            OnGetOpportunitiesOpportunityByOpportunityId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnOpportunitiesOpportunityGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnOpportunitiesOpportunityCreated(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);
        partial void OnAfterOpportunitiesOpportunityCreated(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);

        public async Task<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> CreateOpportunitiesOpportunity(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesopportunity)
        {
            OnOpportunitiesOpportunityCreated(opportunitiesopportunity);

            var existingItem = Context.Opportunitiesopportunities
                              .Where(i => i.opportunity_id == opportunitiesopportunity.opportunity_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Opportunitiesopportunities.Add(opportunitiesopportunity);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(opportunitiesopportunity).State = EntityState.Detached;
                throw;
            }

            OnAfterOpportunitiesOpportunityCreated(opportunitiesopportunity);

            return opportunitiesopportunity;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> CancelOpportunitiesOpportunityChanges(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnOpportunitiesOpportunityUpdated(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);
        partial void OnAfterOpportunitiesOpportunityUpdated(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);

        public async Task<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> UpdateOpportunitiesOpportunity(string opportunityid, EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesopportunity)
        {
            OnOpportunitiesOpportunityUpdated(opportunitiesopportunity);

            var itemToUpdate = Context.Opportunitiesopportunities
                              .Where(i => i.opportunity_id == opportunitiesopportunity.opportunity_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(opportunitiesopportunity);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterOpportunitiesOpportunityUpdated(opportunitiesopportunity);

            return opportunitiesopportunity;
        }

        partial void OnOpportunitiesOpportunityDeleted(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);
        partial void OnAfterOpportunitiesOpportunityDeleted(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);

        public async Task<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> DeleteOpportunitiesOpportunity(string opportunityid)
        {
            var itemToDelete = Context.Opportunitiesopportunities
                              .Where(i => i.opportunity_id == opportunityid)
                              .Include(i => i.Contactscontact_opportunities)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOpportunitiesOpportunityDeleted(itemToDelete);


            Context.Opportunitiesopportunities.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOpportunitiesOpportunityDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTargettargetsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/targettargets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/targettargets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTargettargetsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/targettargets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/targettargets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTargettargetsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TargetTarget> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.TargetTarget>> GetTargettargets(Query query = null)
        {
            var items = Context.Targettargets.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTargettargetsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTargetTargetGet(EspoNew.Server.Models.EspoDbNew.TargetTarget item);
        partial void OnGetTargetTargetByTargetId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TargetTarget> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTarget> GetTargetTargetByTargetId(string targetid)
        {
            var items = Context.Targettargets
                              .AsNoTracking()
                              .Where(i => i.target_id == targetid);

 
            OnGetTargetTargetByTargetId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTargetTargetGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTargetTargetCreated(EspoNew.Server.Models.EspoDbNew.TargetTarget item);
        partial void OnAfterTargetTargetCreated(EspoNew.Server.Models.EspoDbNew.TargetTarget item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTarget> CreateTargetTarget(EspoNew.Server.Models.EspoDbNew.TargetTarget targettarget)
        {
            OnTargetTargetCreated(targettarget);

            var existingItem = Context.Targettargets
                              .Where(i => i.target_id == targettarget.target_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Targettargets.Add(targettarget);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(targettarget).State = EntityState.Detached;
                throw;
            }

            OnAfterTargetTargetCreated(targettarget);

            return targettarget;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTarget> CancelTargetTargetChanges(EspoNew.Server.Models.EspoDbNew.TargetTarget item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTargetTargetUpdated(EspoNew.Server.Models.EspoDbNew.TargetTarget item);
        partial void OnAfterTargetTargetUpdated(EspoNew.Server.Models.EspoDbNew.TargetTarget item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTarget> UpdateTargetTarget(string targetid, EspoNew.Server.Models.EspoDbNew.TargetTarget targettarget)
        {
            OnTargetTargetUpdated(targettarget);

            var itemToUpdate = Context.Targettargets
                              .Where(i => i.target_id == targettarget.target_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(targettarget);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTargetTargetUpdated(targettarget);

            return targettarget;
        }

        partial void OnTargetTargetDeleted(EspoNew.Server.Models.EspoDbNew.TargetTarget item);
        partial void OnAfterTargetTargetDeleted(EspoNew.Server.Models.EspoDbNew.TargetTarget item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTarget> DeleteTargetTarget(string targetid)
        {
            var itemToDelete = Context.Targettargets
                              .Where(i => i.target_id == targetid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTargetTargetDeleted(itemToDelete);


            Context.Targettargets.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTargetTargetDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTargettarget_listsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/targettarget_lists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/targettarget_lists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTargettarget_listsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/targettarget_lists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/targettarget_lists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTargettarget_listsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TargetTargetList> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.TargetTargetList>> GetTargettarget_lists(Query query = null)
        {
            var items = Context.Targettarget_lists.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTargettarget_listsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTargetTargetListGet(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);
        partial void OnGetTargetTargetListByTargetListId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TargetTargetList> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTargetList> GetTargetTargetListByTargetListId(string targetlistid)
        {
            var items = Context.Targettarget_lists
                              .AsNoTracking()
                              .Where(i => i.target_list_id == targetlistid);

 
            OnGetTargetTargetListByTargetListId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTargetTargetListGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTargetTargetListCreated(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);
        partial void OnAfterTargetTargetListCreated(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTargetList> CreateTargetTargetList(EspoNew.Server.Models.EspoDbNew.TargetTargetList targettargetlist)
        {
            OnTargetTargetListCreated(targettargetlist);

            var existingItem = Context.Targettarget_lists
                              .Where(i => i.target_list_id == targettargetlist.target_list_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Targettarget_lists.Add(targettargetlist);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(targettargetlist).State = EntityState.Detached;
                throw;
            }

            OnAfterTargetTargetListCreated(targettargetlist);

            return targettargetlist;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTargetList> CancelTargetTargetListChanges(EspoNew.Server.Models.EspoDbNew.TargetTargetList item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTargetTargetListUpdated(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);
        partial void OnAfterTargetTargetListUpdated(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTargetList> UpdateTargetTargetList(string targetlistid, EspoNew.Server.Models.EspoDbNew.TargetTargetList targettargetlist)
        {
            OnTargetTargetListUpdated(targettargetlist);

            var itemToUpdate = Context.Targettarget_lists
                              .Where(i => i.target_list_id == targettargetlist.target_list_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(targettargetlist);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTargetTargetListUpdated(targettargetlist);

            return targettargetlist;
        }

        partial void OnTargetTargetListDeleted(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);
        partial void OnAfterTargetTargetListDeleted(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTargetList> DeleteTargetTargetList(string targetlistid)
        {
            var itemToDelete = Context.Targettarget_lists
                              .Where(i => i.target_list_id == targetlistid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTargetTargetListDeleted(itemToDelete);


            Context.Targettarget_lists.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTargetTargetListDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTaskstasksToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/taskstasks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/taskstasks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTaskstasksToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/taskstasks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/taskstasks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTaskstasksRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TasksTask> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.TasksTask>> GetTaskstasks(Query query = null)
        {
            var items = Context.Taskstasks.AsQueryable();

            items = items.Include(i => i.account);
            items = items.Include(i => i.employee);
            items = items.Include(i => i.contact);
            items = items.Include(i => i.email);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTaskstasksRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTasksTaskGet(EspoNew.Server.Models.EspoDbNew.TasksTask item);
        partial void OnGetTasksTaskByTaskId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TasksTask> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.TasksTask> GetTasksTaskByTaskId(string taskid)
        {
            var items = Context.Taskstasks
                              .AsNoTracking()
                              .Where(i => i.task_id == taskid);

            items = items.Include(i => i.account);
            items = items.Include(i => i.employee);
            items = items.Include(i => i.contact);
            items = items.Include(i => i.email);
 
            OnGetTasksTaskByTaskId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTasksTaskGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTasksTaskCreated(EspoNew.Server.Models.EspoDbNew.TasksTask item);
        partial void OnAfterTasksTaskCreated(EspoNew.Server.Models.EspoDbNew.TasksTask item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TasksTask> CreateTasksTask(EspoNew.Server.Models.EspoDbNew.TasksTask taskstask)
        {
            OnTasksTaskCreated(taskstask);

            var existingItem = Context.Taskstasks
                              .Where(i => i.task_id == taskstask.task_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Taskstasks.Add(taskstask);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(taskstask).State = EntityState.Detached;
                throw;
            }

            OnAfterTasksTaskCreated(taskstask);

            return taskstask;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.TasksTask> CancelTasksTaskChanges(EspoNew.Server.Models.EspoDbNew.TasksTask item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTasksTaskUpdated(EspoNew.Server.Models.EspoDbNew.TasksTask item);
        partial void OnAfterTasksTaskUpdated(EspoNew.Server.Models.EspoDbNew.TasksTask item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TasksTask> UpdateTasksTask(string taskid, EspoNew.Server.Models.EspoDbNew.TasksTask taskstask)
        {
            OnTasksTaskUpdated(taskstask);

            var itemToUpdate = Context.Taskstasks
                              .Where(i => i.task_id == taskstask.task_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(taskstask);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTasksTaskUpdated(taskstask);

            return taskstask;
        }

        partial void OnTasksTaskDeleted(EspoNew.Server.Models.EspoDbNew.TasksTask item);
        partial void OnAfterTasksTaskDeleted(EspoNew.Server.Models.EspoDbNew.TasksTask item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TasksTask> DeleteTasksTask(string taskid)
        {
            var itemToDelete = Context.Taskstasks
                              .Where(i => i.task_id == taskid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTasksTaskDeleted(itemToDelete);


            Context.Taskstasks.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTasksTaskDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTeamsteamsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/teamsteams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/teamsteams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTeamsteamsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/teamsteams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/teamsteams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTeamsteamsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TeamsTeam> items);

        public async Task<IQueryable<EspoNew.Server.Models.EspoDbNew.TeamsTeam>> GetTeamsteams(Query query = null)
        {
            var items = Context.Teamsteams.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTeamsteamsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTeamsTeamGet(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);
        partial void OnGetTeamsTeamByTeamId(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TeamsTeam> items);


        public async Task<EspoNew.Server.Models.EspoDbNew.TeamsTeam> GetTeamsTeamByTeamId(string teamid)
        {
            var items = Context.Teamsteams
                              .AsNoTracking()
                              .Where(i => i.team_id == teamid);

 
            OnGetTeamsTeamByTeamId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTeamsTeamGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTeamsTeamCreated(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);
        partial void OnAfterTeamsTeamCreated(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TeamsTeam> CreateTeamsTeam(EspoNew.Server.Models.EspoDbNew.TeamsTeam teamsteam)
        {
            OnTeamsTeamCreated(teamsteam);

            var existingItem = Context.Teamsteams
                              .Where(i => i.team_id == teamsteam.team_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Teamsteams.Add(teamsteam);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(teamsteam).State = EntityState.Detached;
                throw;
            }

            OnAfterTeamsTeamCreated(teamsteam);

            return teamsteam;
        }

        public async Task<EspoNew.Server.Models.EspoDbNew.TeamsTeam> CancelTeamsTeamChanges(EspoNew.Server.Models.EspoDbNew.TeamsTeam item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTeamsTeamUpdated(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);
        partial void OnAfterTeamsTeamUpdated(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TeamsTeam> UpdateTeamsTeam(string teamid, EspoNew.Server.Models.EspoDbNew.TeamsTeam teamsteam)
        {
            OnTeamsTeamUpdated(teamsteam);

            var itemToUpdate = Context.Teamsteams
                              .Where(i => i.team_id == teamsteam.team_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(teamsteam);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTeamsTeamUpdated(teamsteam);

            return teamsteam;
        }

        partial void OnTeamsTeamDeleted(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);
        partial void OnAfterTeamsTeamDeleted(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);

        public async Task<EspoNew.Server.Models.EspoDbNew.TeamsTeam> DeleteTeamsTeam(string teamid)
        {
            var itemToDelete = Context.Teamsteams
                              .Where(i => i.team_id == teamid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTeamsTeamDeleted(itemToDelete);


            Context.Teamsteams.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTeamsTeamDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}