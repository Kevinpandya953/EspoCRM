
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace EspoNew.Client
{
    public partial class EspoDbNewService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public EspoDbNewService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/EspoDbNew/");
        }


        public async System.Threading.Tasks.Task ExportAccountsaccountsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAccountsaccountsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAccountsaccounts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AccountsAccount>> GetAccountsaccounts(Query query)
        {
            return await GetAccountsaccounts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AccountsAccount>> GetAccountsaccounts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Accountsaccounts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAccountsaccounts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AccountsAccount>>(response);
        }

        partial void OnCreateAccountsAccount(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccount> CreateAccountsAccount(EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsAccount = default(EspoNew.Server.Models.EspoDbNew.AccountsAccount))
        {
            var uri = new Uri(baseUri, $"Accountsaccounts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(accountsAccount), Encoding.UTF8, "application/json");

            OnCreateAccountsAccount(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AccountsAccount>(response);
        }

        partial void OnDeleteAccountsAccount(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAccountsAccount(string accountId = default(string))
        {
            var uri = new Uri(baseUri, $"Accountsaccounts('{Uri.EscapeDataString(accountId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAccountsAccount(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAccountsAccountByAccountId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccount> GetAccountsAccountByAccountId(string expand = default(string), string accountId = default(string))
        {
            var uri = new Uri(baseUri, $"Accountsaccounts('{Uri.EscapeDataString(accountId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAccountsAccountByAccountId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AccountsAccount>(response);
        }

        partial void OnUpdateAccountsAccount(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAccountsAccount(string accountId = default(string), EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsAccount = default(EspoNew.Server.Models.EspoDbNew.AccountsAccount))
        {
            var uri = new Uri(baseUri, $"Accountsaccounts('{Uri.EscapeDataString(accountId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", accountsAccount.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(accountsAccount), Encoding.UTF8, "application/json");

            OnUpdateAccountsAccount(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAccountsaccount_contactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccount_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccount_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAccountsaccount_contactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccount_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccount_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAccountsaccount_contacts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>> GetAccountsaccount_contacts(Query query)
        {
            return await GetAccountsaccount_contacts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>> GetAccountsaccount_contacts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_contacts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAccountsaccount_contacts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>>(response);
        }

        partial void OnCreateAccountsAccountContact(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> CreateAccountsAccountContact(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact accountsAccountContact = default(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_contacts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(accountsAccountContact), Encoding.UTF8, "application/json");

            OnCreateAccountsAccountContact(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>(response);
        }

        partial void OnDeleteAccountsAccountContact(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAccountsAccountContact(string accountContactId = default(string))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_contacts('{Uri.EscapeDataString(accountContactId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAccountsAccountContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAccountsAccountContactByAccountContactId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> GetAccountsAccountContactByAccountContactId(string expand = default(string), string accountContactId = default(string))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_contacts('{Uri.EscapeDataString(accountContactId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAccountsAccountContactByAccountContactId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>(response);
        }

        partial void OnUpdateAccountsAccountContact(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAccountsAccountContact(string accountContactId = default(string), EspoNew.Server.Models.EspoDbNew.AccountsAccountContact accountsAccountContact = default(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_contacts('{Uri.EscapeDataString(accountContactId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", accountsAccountContact.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(accountsAccountContact), Encoding.UTF8, "application/json");

            OnUpdateAccountsAccountContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAccountsaccount_documentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccount_documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccount_documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAccountsaccount_documentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/accountsaccount_documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/accountsaccount_documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAccountsaccount_documents(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>> GetAccountsaccount_documents(Query query)
        {
            return await GetAccountsaccount_documents(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>> GetAccountsaccount_documents(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_documents");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAccountsaccount_documents(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>>(response);
        }

        partial void OnCreateAccountsAccountDocument(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> CreateAccountsAccountDocument(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument accountsAccountDocument = default(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_documents");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(accountsAccountDocument), Encoding.UTF8, "application/json");

            OnCreateAccountsAccountDocument(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>(response);
        }

        partial void OnDeleteAccountsAccountDocument(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAccountsAccountDocument(string accountDocumentId = default(string))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_documents('{Uri.EscapeDataString(accountDocumentId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAccountsAccountDocument(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAccountsAccountDocumentByAccountDocumentId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> GetAccountsAccountDocumentByAccountDocumentId(string expand = default(string), string accountDocumentId = default(string))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_documents('{Uri.EscapeDataString(accountDocumentId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAccountsAccountDocumentByAccountDocumentId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>(response);
        }

        partial void OnUpdateAccountsAccountDocument(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAccountsAccountDocument(string accountDocumentId = default(string), EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument accountsAccountDocument = default(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument))
        {
            var uri = new Uri(baseUri, $"Accountsaccount_documents('{Uri.EscapeDataString(accountDocumentId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", accountsAccountDocument.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(accountsAccountDocument), Encoding.UTF8, "application/json");

            OnUpdateAccountsAccountDocument(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAddressaddress_citiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAddressaddress_citiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAddressaddress_cities(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>> GetAddressaddress_cities(Query query)
        {
            return await GetAddressaddress_cities(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>> GetAddressaddress_cities(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Addressaddress_cities");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAddressaddress_cities(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>>(response);
        }

        partial void OnCreateAddressAddressCity(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> CreateAddressAddressCity(EspoNew.Server.Models.EspoDbNew.AddressAddressCity addressAddressCity = default(EspoNew.Server.Models.EspoDbNew.AddressAddressCity))
        {
            var uri = new Uri(baseUri, $"Addressaddress_cities");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(addressAddressCity), Encoding.UTF8, "application/json");

            OnCreateAddressAddressCity(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>(response);
        }

        partial void OnDeleteAddressAddressCity(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAddressAddressCity(string cityId = default(string))
        {
            var uri = new Uri(baseUri, $"Addressaddress_cities('{Uri.EscapeDataString(cityId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAddressAddressCity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAddressAddressCityByCityId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> GetAddressAddressCityByCityId(string expand = default(string), string cityId = default(string))
        {
            var uri = new Uri(baseUri, $"Addressaddress_cities('{Uri.EscapeDataString(cityId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAddressAddressCityByCityId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>(response);
        }

        partial void OnUpdateAddressAddressCity(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAddressAddressCity(string cityId = default(string), EspoNew.Server.Models.EspoDbNew.AddressAddressCity addressAddressCity = default(EspoNew.Server.Models.EspoDbNew.AddressAddressCity))
        {
            var uri = new Uri(baseUri, $"Addressaddress_cities('{Uri.EscapeDataString(cityId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", addressAddressCity.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(addressAddressCity), Encoding.UTF8, "application/json");

            OnUpdateAddressAddressCity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAddressaddress_countriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAddressaddress_countriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAddressaddress_countries(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>> GetAddressaddress_countries(Query query)
        {
            return await GetAddressaddress_countries(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>> GetAddressaddress_countries(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Addressaddress_countries");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAddressaddress_countries(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>>(response);
        }

        partial void OnCreateAddressAddressCountry(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> CreateAddressAddressCountry(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressAddressCountry = default(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry))
        {
            var uri = new Uri(baseUri, $"Addressaddress_countries");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(addressAddressCountry), Encoding.UTF8, "application/json");

            OnCreateAddressAddressCountry(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>(response);
        }

        partial void OnDeleteAddressAddressCountry(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAddressAddressCountry(string countryId = default(string))
        {
            var uri = new Uri(baseUri, $"Addressaddress_countries('{Uri.EscapeDataString(countryId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAddressAddressCountry(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAddressAddressCountryByCountryId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> GetAddressAddressCountryByCountryId(string expand = default(string), string countryId = default(string))
        {
            var uri = new Uri(baseUri, $"Addressaddress_countries('{Uri.EscapeDataString(countryId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAddressAddressCountryByCountryId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>(response);
        }

        partial void OnUpdateAddressAddressCountry(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAddressAddressCountry(string countryId = default(string), EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressAddressCountry = default(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry))
        {
            var uri = new Uri(baseUri, $"Addressaddress_countries('{Uri.EscapeDataString(countryId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", addressAddressCountry.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(addressAddressCountry), Encoding.UTF8, "application/json");

            OnUpdateAddressAddressCountry(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAddressaddress_statesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_states/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_states/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAddressaddress_statesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/addressaddress_states/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/addressaddress_states/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAddressaddress_states(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AddressAddressState>> GetAddressaddress_states(Query query)
        {
            return await GetAddressaddress_states(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AddressAddressState>> GetAddressaddress_states(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Addressaddress_states");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAddressaddress_states(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.AddressAddressState>>(response);
        }

        partial void OnCreateAddressAddressState(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressState> CreateAddressAddressState(EspoNew.Server.Models.EspoDbNew.AddressAddressState addressAddressState = default(EspoNew.Server.Models.EspoDbNew.AddressAddressState))
        {
            var uri = new Uri(baseUri, $"Addressaddress_states");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(addressAddressState), Encoding.UTF8, "application/json");

            OnCreateAddressAddressState(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AddressAddressState>(response);
        }

        partial void OnDeleteAddressAddressState(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAddressAddressState(string stateId = default(string))
        {
            var uri = new Uri(baseUri, $"Addressaddress_states('{Uri.EscapeDataString(stateId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAddressAddressState(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAddressAddressStateByStateId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.AddressAddressState> GetAddressAddressStateByStateId(string expand = default(string), string stateId = default(string))
        {
            var uri = new Uri(baseUri, $"Addressaddress_states('{Uri.EscapeDataString(stateId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAddressAddressStateByStateId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.AddressAddressState>(response);
        }

        partial void OnUpdateAddressAddressState(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAddressAddressState(string stateId = default(string), EspoNew.Server.Models.EspoDbNew.AddressAddressState addressAddressState = default(EspoNew.Server.Models.EspoDbNew.AddressAddressState))
        {
            var uri = new Uri(baseUri, $"Addressaddress_states('{Uri.EscapeDataString(stateId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", addressAddressState.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(addressAddressState), Encoding.UTF8, "application/json");

            OnUpdateAddressAddressState(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCallscallsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscalls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscalls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCallscallsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscalls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscalls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCallscalls(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CallsCall>> GetCallscalls(Query query)
        {
            return await GetCallscalls(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CallsCall>> GetCallscalls(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Callscalls");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCallscalls(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CallsCall>>(response);
        }

        partial void OnCreateCallsCall(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCall> CreateCallsCall(EspoNew.Server.Models.EspoDbNew.CallsCall callsCall = default(EspoNew.Server.Models.EspoDbNew.CallsCall))
        {
            var uri = new Uri(baseUri, $"Callscalls");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(callsCall), Encoding.UTF8, "application/json");

            OnCreateCallsCall(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CallsCall>(response);
        }

        partial void OnDeleteCallsCall(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCallsCall(string callId = default(string))
        {
            var uri = new Uri(baseUri, $"Callscalls('{Uri.EscapeDataString(callId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCallsCall(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCallsCallByCallId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCall> GetCallsCallByCallId(string expand = default(string), string callId = default(string))
        {
            var uri = new Uri(baseUri, $"Callscalls('{Uri.EscapeDataString(callId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCallsCallByCallId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CallsCall>(response);
        }

        partial void OnUpdateCallsCall(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCallsCall(string callId = default(string), EspoNew.Server.Models.EspoDbNew.CallsCall callsCall = default(EspoNew.Server.Models.EspoDbNew.CallsCall))
        {
            var uri = new Uri(baseUri, $"Callscalls('{Uri.EscapeDataString(callId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", callsCall.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(callsCall), Encoding.UTF8, "application/json");

            OnUpdateCallsCall(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCallscall_contactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscall_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscall_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCallscall_contactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscall_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscall_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCallscall_contacts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CallsCallContact>> GetCallscall_contacts(Query query)
        {
            return await GetCallscall_contacts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CallsCallContact>> GetCallscall_contacts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Callscall_contacts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCallscall_contacts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CallsCallContact>>(response);
        }

        partial void OnCreateCallsCallContact(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallContact> CreateCallsCallContact(EspoNew.Server.Models.EspoDbNew.CallsCallContact callsCallContact = default(EspoNew.Server.Models.EspoDbNew.CallsCallContact))
        {
            var uri = new Uri(baseUri, $"Callscall_contacts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(callsCallContact), Encoding.UTF8, "application/json");

            OnCreateCallsCallContact(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CallsCallContact>(response);
        }

        partial void OnDeleteCallsCallContact(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCallsCallContact(string callContactId = default(string))
        {
            var uri = new Uri(baseUri, $"Callscall_contacts('{Uri.EscapeDataString(callContactId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCallsCallContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCallsCallContactByCallContactId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallContact> GetCallsCallContactByCallContactId(string expand = default(string), string callContactId = default(string))
        {
            var uri = new Uri(baseUri, $"Callscall_contacts('{Uri.EscapeDataString(callContactId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCallsCallContactByCallContactId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CallsCallContact>(response);
        }

        partial void OnUpdateCallsCallContact(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCallsCallContact(string callContactId = default(string), EspoNew.Server.Models.EspoDbNew.CallsCallContact callsCallContact = default(EspoNew.Server.Models.EspoDbNew.CallsCallContact))
        {
            var uri = new Uri(baseUri, $"Callscall_contacts('{Uri.EscapeDataString(callContactId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", callsCallContact.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(callsCallContact), Encoding.UTF8, "application/json");

            OnUpdateCallsCallContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCallscall_leadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscall_leads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscall_leads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCallscall_leadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/callscall_leads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/callscall_leads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCallscall_leads(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CallsCallLead>> GetCallscall_leads(Query query)
        {
            return await GetCallscall_leads(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CallsCallLead>> GetCallscall_leads(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Callscall_leads");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCallscall_leads(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CallsCallLead>>(response);
        }

        partial void OnCreateCallsCallLead(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallLead> CreateCallsCallLead(EspoNew.Server.Models.EspoDbNew.CallsCallLead callsCallLead = default(EspoNew.Server.Models.EspoDbNew.CallsCallLead))
        {
            var uri = new Uri(baseUri, $"Callscall_leads");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(callsCallLead), Encoding.UTF8, "application/json");

            OnCreateCallsCallLead(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CallsCallLead>(response);
        }

        partial void OnDeleteCallsCallLead(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCallsCallLead(string callLeadId = default(string))
        {
            var uri = new Uri(baseUri, $"Callscall_leads('{Uri.EscapeDataString(callLeadId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCallsCallLead(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCallsCallLeadByCallLeadId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CallsCallLead> GetCallsCallLeadByCallLeadId(string expand = default(string), string callLeadId = default(string))
        {
            var uri = new Uri(baseUri, $"Callscall_leads('{Uri.EscapeDataString(callLeadId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCallsCallLeadByCallLeadId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CallsCallLead>(response);
        }

        partial void OnUpdateCallsCallLead(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCallsCallLead(string callLeadId = default(string), EspoNew.Server.Models.EspoDbNew.CallsCallLead callsCallLead = default(EspoNew.Server.Models.EspoDbNew.CallsCallLead))
        {
            var uri = new Uri(baseUri, $"Callscall_leads('{Uri.EscapeDataString(callLeadId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", callsCallLead.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(callsCallLead), Encoding.UTF8, "application/json");

            OnUpdateCallsCallLead(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCampaigncampaignsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/campaigncampaigns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/campaigncampaigns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCampaigncampaignsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/campaigncampaigns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/campaigncampaigns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCampaigncampaigns(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>> GetCampaigncampaigns(Query query)
        {
            return await GetCampaigncampaigns(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>> GetCampaigncampaigns(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Campaigncampaigns");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCampaigncampaigns(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>>(response);
        }

        partial void OnCreateCampaignCampaign(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> CreateCampaignCampaign(EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaignCampaign = default(EspoNew.Server.Models.EspoDbNew.CampaignCampaign))
        {
            var uri = new Uri(baseUri, $"Campaigncampaigns");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(campaignCampaign), Encoding.UTF8, "application/json");

            OnCreateCampaignCampaign(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>(response);
        }

        partial void OnDeleteCampaignCampaign(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCampaignCampaign(string campaignId = default(string))
        {
            var uri = new Uri(baseUri, $"Campaigncampaigns('{Uri.EscapeDataString(campaignId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCampaignCampaign(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCampaignCampaignByCampaignId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> GetCampaignCampaignByCampaignId(string expand = default(string), string campaignId = default(string))
        {
            var uri = new Uri(baseUri, $"Campaigncampaigns('{Uri.EscapeDataString(campaignId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCampaignCampaignByCampaignId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>(response);
        }

        partial void OnUpdateCampaignCampaign(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCampaignCampaign(string campaignId = default(string), EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaignCampaign = default(EspoNew.Server.Models.EspoDbNew.CampaignCampaign))
        {
            var uri = new Uri(baseUri, $"Campaigncampaigns('{Uri.EscapeDataString(campaignId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", campaignCampaign.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(campaignCampaign), Encoding.UTF8, "application/json");

            OnUpdateCampaignCampaign(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCases_casesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/cases_cases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/cases_cases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCases_casesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/cases_cases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/cases_cases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCases_cases(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CasesCase>> GetCases_cases(Query query)
        {
            return await GetCases_cases(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CasesCase>> GetCases_cases(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Cases_cases");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCases_cases(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CasesCase>>(response);
        }

        partial void OnCreateCasesCase(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCase> CreateCasesCase(EspoNew.Server.Models.EspoDbNew.CasesCase casesCase = default(EspoNew.Server.Models.EspoDbNew.CasesCase))
        {
            var uri = new Uri(baseUri, $"Cases_cases");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(casesCase), Encoding.UTF8, "application/json");

            OnCreateCasesCase(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CasesCase>(response);
        }

        partial void OnDeleteCasesCase(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCasesCase(string caseId = default(string))
        {
            var uri = new Uri(baseUri, $"Cases_cases('{Uri.EscapeDataString(caseId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCasesCase(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCasesCaseByCaseId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCase> GetCasesCaseByCaseId(string expand = default(string), string caseId = default(string))
        {
            var uri = new Uri(baseUri, $"Cases_cases('{Uri.EscapeDataString(caseId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCasesCaseByCaseId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CasesCase>(response);
        }

        partial void OnUpdateCasesCase(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCasesCase(string caseId = default(string), EspoNew.Server.Models.EspoDbNew.CasesCase casesCase = default(EspoNew.Server.Models.EspoDbNew.CasesCase))
        {
            var uri = new Uri(baseUri, $"Cases_cases('{Uri.EscapeDataString(caseId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", casesCase.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(casesCase), Encoding.UTF8, "application/json");

            OnUpdateCasesCase(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCasescase_contactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/casescase_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/casescase_contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCasescase_contactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/casescase_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/casescase_contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCasescase_contacts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>> GetCasescase_contacts(Query query)
        {
            return await GetCasescase_contacts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>> GetCasescase_contacts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Casescase_contacts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCasescase_contacts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>>(response);
        }

        partial void OnCreateCasesCaseContact(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> CreateCasesCaseContact(EspoNew.Server.Models.EspoDbNew.CasesCaseContact casesCaseContact = default(EspoNew.Server.Models.EspoDbNew.CasesCaseContact))
        {
            var uri = new Uri(baseUri, $"Casescase_contacts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(casesCaseContact), Encoding.UTF8, "application/json");

            OnCreateCasesCaseContact(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>(response);
        }

        partial void OnDeleteCasesCaseContact(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCasesCaseContact(string caseContactId = default(string))
        {
            var uri = new Uri(baseUri, $"Casescase_contacts('{Uri.EscapeDataString(caseContactId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCasesCaseContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCasesCaseContactByCaseContactId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> GetCasesCaseContactByCaseContactId(string expand = default(string), string caseContactId = default(string))
        {
            var uri = new Uri(baseUri, $"Casescase_contacts('{Uri.EscapeDataString(caseContactId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCasesCaseContactByCaseContactId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>(response);
        }

        partial void OnUpdateCasesCaseContact(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCasesCaseContact(string caseContactId = default(string), EspoNew.Server.Models.EspoDbNew.CasesCaseContact casesCaseContact = default(EspoNew.Server.Models.EspoDbNew.CasesCaseContact))
        {
            var uri = new Uri(baseUri, $"Casescase_contacts('{Uri.EscapeDataString(caseContactId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", casesCaseContact.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(casesCaseContact), Encoding.UTF8, "application/json");

            OnUpdateCasesCaseContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportContactscontactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContactscontactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetContactscontacts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContact>> GetContactscontacts(Query query)
        {
            return await GetContactscontacts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContact>> GetContactscontacts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontacts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactscontacts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContact>>(response);
        }

        partial void OnCreateContactsContact(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContact> CreateContactsContact(EspoNew.Server.Models.EspoDbNew.ContactsContact contactsContact = default(EspoNew.Server.Models.EspoDbNew.ContactsContact))
        {
            var uri = new Uri(baseUri, $"Contactscontacts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactsContact), Encoding.UTF8, "application/json");

            OnCreateContactsContact(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.ContactsContact>(response);
        }

        partial void OnDeleteContactsContact(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteContactsContact(string contactId = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontacts('{Uri.EscapeDataString(contactId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContactsContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetContactsContactByContactId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContact> GetContactsContactByContactId(string expand = default(string), string contactId = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontacts('{Uri.EscapeDataString(contactId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactsContactByContactId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.ContactsContact>(response);
        }

        partial void OnUpdateContactsContact(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateContactsContact(string contactId = default(string), EspoNew.Server.Models.EspoDbNew.ContactsContact contactsContact = default(EspoNew.Server.Models.EspoDbNew.ContactsContact))
        {
            var uri = new Uri(baseUri, $"Contactscontacts('{Uri.EscapeDataString(contactId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", contactsContact.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactsContact), Encoding.UTF8, "application/json");

            OnUpdateContactsContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportContactscontact_documentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContactscontact_documentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetContactscontact_documents(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>> GetContactscontact_documents(Query query)
        {
            return await GetContactscontact_documents(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>> GetContactscontact_documents(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontact_documents");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactscontact_documents(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>>(response);
        }

        partial void OnCreateContactsContactDocument(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> CreateContactsContactDocument(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument contactsContactDocument = default(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument))
        {
            var uri = new Uri(baseUri, $"Contactscontact_documents");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactsContactDocument), Encoding.UTF8, "application/json");

            OnCreateContactsContactDocument(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>(response);
        }

        partial void OnDeleteContactsContactDocument(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteContactsContactDocument(string contactDocumentId = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontact_documents('{Uri.EscapeDataString(contactDocumentId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContactsContactDocument(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetContactsContactDocumentByContactDocumentId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> GetContactsContactDocumentByContactDocumentId(string expand = default(string), string contactDocumentId = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontact_documents('{Uri.EscapeDataString(contactDocumentId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactsContactDocumentByContactDocumentId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>(response);
        }

        partial void OnUpdateContactsContactDocument(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateContactsContactDocument(string contactDocumentId = default(string), EspoNew.Server.Models.EspoDbNew.ContactsContactDocument contactsContactDocument = default(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument))
        {
            var uri = new Uri(baseUri, $"Contactscontact_documents('{Uri.EscapeDataString(contactDocumentId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", contactsContactDocument.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactsContactDocument), Encoding.UTF8, "application/json");

            OnUpdateContactsContactDocument(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportContactscontact_meetingsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_meetings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_meetings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContactscontact_meetingsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_meetings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_meetings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetContactscontact_meetings(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>> GetContactscontact_meetings(Query query)
        {
            return await GetContactscontact_meetings(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>> GetContactscontact_meetings(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontact_meetings");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactscontact_meetings(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>>(response);
        }

        partial void OnCreateContactsContactMeeting(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> CreateContactsContactMeeting(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting contactsContactMeeting = default(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting))
        {
            var uri = new Uri(baseUri, $"Contactscontact_meetings");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactsContactMeeting), Encoding.UTF8, "application/json");

            OnCreateContactsContactMeeting(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>(response);
        }

        partial void OnDeleteContactsContactMeeting(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteContactsContactMeeting(string contactMeetingId = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontact_meetings('{Uri.EscapeDataString(contactMeetingId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContactsContactMeeting(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetContactsContactMeetingByContactMeetingId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> GetContactsContactMeetingByContactMeetingId(string expand = default(string), string contactMeetingId = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontact_meetings('{Uri.EscapeDataString(contactMeetingId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactsContactMeetingByContactMeetingId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>(response);
        }

        partial void OnUpdateContactsContactMeeting(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateContactsContactMeeting(string contactMeetingId = default(string), EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting contactsContactMeeting = default(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting))
        {
            var uri = new Uri(baseUri, $"Contactscontact_meetings('{Uri.EscapeDataString(contactMeetingId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", contactsContactMeeting.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactsContactMeeting), Encoding.UTF8, "application/json");

            OnUpdateContactsContactMeeting(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportContactscontact_opportunitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContactscontact_opportunitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/contactscontact_opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/contactscontact_opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetContactscontact_opportunities(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>> GetContactscontact_opportunities(Query query)
        {
            return await GetContactscontact_opportunities(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>> GetContactscontact_opportunities(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontact_opportunities");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactscontact_opportunities(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>>(response);
        }

        partial void OnCreateContactsContactOpportunity(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> CreateContactsContactOpportunity(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity contactsContactOpportunity = default(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity))
        {
            var uri = new Uri(baseUri, $"Contactscontact_opportunities");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactsContactOpportunity), Encoding.UTF8, "application/json");

            OnCreateContactsContactOpportunity(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>(response);
        }

        partial void OnDeleteContactsContactOpportunity(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteContactsContactOpportunity(string contactOpportunityId = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontact_opportunities('{Uri.EscapeDataString(contactOpportunityId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContactsContactOpportunity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetContactsContactOpportunityByContactOpportunityId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> GetContactsContactOpportunityByContactOpportunityId(string expand = default(string), string contactOpportunityId = default(string))
        {
            var uri = new Uri(baseUri, $"Contactscontact_opportunities('{Uri.EscapeDataString(contactOpportunityId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactsContactOpportunityByContactOpportunityId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>(response);
        }

        partial void OnUpdateContactsContactOpportunity(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateContactsContactOpportunity(string contactOpportunityId = default(string), EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity contactsContactOpportunity = default(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity))
        {
            var uri = new Uri(baseUri, $"Contactscontact_opportunities('{Uri.EscapeDataString(contactOpportunityId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", contactsContactOpportunity.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contactsContactOpportunity), Encoding.UTF8, "application/json");

            OnUpdateContactsContactOpportunity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDocumentsdocumentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/documentsdocuments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/documentsdocuments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDocumentsdocumentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/documentsdocuments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/documentsdocuments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDocumentsdocuments(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>> GetDocumentsdocuments(Query query)
        {
            return await GetDocumentsdocuments(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>> GetDocumentsdocuments(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Documentsdocuments");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDocumentsdocuments(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>>(response);
        }

        partial void OnCreateDocumentsDocument(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> CreateDocumentsDocument(EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsDocument = default(EspoNew.Server.Models.EspoDbNew.DocumentsDocument))
        {
            var uri = new Uri(baseUri, $"Documentsdocuments");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(documentsDocument), Encoding.UTF8, "application/json");

            OnCreateDocumentsDocument(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>(response);
        }

        partial void OnDeleteDocumentsDocument(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDocumentsDocument(string documentId = default(string))
        {
            var uri = new Uri(baseUri, $"Documentsdocuments('{Uri.EscapeDataString(documentId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDocumentsDocument(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDocumentsDocumentByDocumentId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> GetDocumentsDocumentByDocumentId(string expand = default(string), string documentId = default(string))
        {
            var uri = new Uri(baseUri, $"Documentsdocuments('{Uri.EscapeDataString(documentId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDocumentsDocumentByDocumentId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>(response);
        }

        partial void OnUpdateDocumentsDocument(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDocumentsDocument(string documentId = default(string), EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsDocument = default(EspoNew.Server.Models.EspoDbNew.DocumentsDocument))
        {
            var uri = new Uri(baseUri, $"Documentsdocuments('{Uri.EscapeDataString(documentId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", documentsDocument.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(documentsDocument), Encoding.UTF8, "application/json");

            OnUpdateDocumentsDocument(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDocumentsdocument_leadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/documentsdocument_leads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/documentsdocument_leads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDocumentsdocument_leadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/documentsdocument_leads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/documentsdocument_leads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDocumentsdocument_leads(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>> GetDocumentsdocument_leads(Query query)
        {
            return await GetDocumentsdocument_leads(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>> GetDocumentsdocument_leads(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Documentsdocument_leads");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDocumentsdocument_leads(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>>(response);
        }

        partial void OnCreateDocumentsDocumentLead(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> CreateDocumentsDocumentLead(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead documentsDocumentLead = default(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead))
        {
            var uri = new Uri(baseUri, $"Documentsdocument_leads");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(documentsDocumentLead), Encoding.UTF8, "application/json");

            OnCreateDocumentsDocumentLead(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>(response);
        }

        partial void OnDeleteDocumentsDocumentLead(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDocumentsDocumentLead(string documentLeadId = default(string))
        {
            var uri = new Uri(baseUri, $"Documentsdocument_leads('{Uri.EscapeDataString(documentLeadId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDocumentsDocumentLead(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDocumentsDocumentLeadByDocumentLeadId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> GetDocumentsDocumentLeadByDocumentLeadId(string expand = default(string), string documentLeadId = default(string))
        {
            var uri = new Uri(baseUri, $"Documentsdocument_leads('{Uri.EscapeDataString(documentLeadId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDocumentsDocumentLeadByDocumentLeadId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>(response);
        }

        partial void OnUpdateDocumentsDocumentLead(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDocumentsDocumentLead(string documentLeadId = default(string), EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead documentsDocumentLead = default(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead))
        {
            var uri = new Uri(baseUri, $"Documentsdocument_leads('{Uri.EscapeDataString(documentLeadId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", documentsDocumentLead.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(documentsDocumentLead), Encoding.UTF8, "application/json");

            OnUpdateDocumentsDocumentLead(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportEmailemailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/emailemails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/emailemails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEmailemailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/emailemails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/emailemails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEmailemails(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.EmailEmail>> GetEmailemails(Query query)
        {
            return await GetEmailemails(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.EmailEmail>> GetEmailemails(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Emailemails");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmailemails(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.EmailEmail>>(response);
        }

        partial void OnCreateEmailEmail(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmail> CreateEmailEmail(EspoNew.Server.Models.EspoDbNew.EmailEmail emailEmail = default(EspoNew.Server.Models.EspoDbNew.EmailEmail))
        {
            var uri = new Uri(baseUri, $"Emailemails");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(emailEmail), Encoding.UTF8, "application/json");

            OnCreateEmailEmail(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.EmailEmail>(response);
        }

        partial void OnDeleteEmailEmail(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEmailEmail(string emailId = default(string))
        {
            var uri = new Uri(baseUri, $"Emailemails('{Uri.EscapeDataString(emailId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEmailEmail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEmailEmailByEmailId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmail> GetEmailEmailByEmailId(string expand = default(string), string emailId = default(string))
        {
            var uri = new Uri(baseUri, $"Emailemails('{Uri.EscapeDataString(emailId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmailEmailByEmailId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.EmailEmail>(response);
        }

        partial void OnUpdateEmailEmail(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEmailEmail(string emailId = default(string), EspoNew.Server.Models.EspoDbNew.EmailEmail emailEmail = default(EspoNew.Server.Models.EspoDbNew.EmailEmail))
        {
            var uri = new Uri(baseUri, $"Emailemails('{Uri.EscapeDataString(emailId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", emailEmail.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(emailEmail), Encoding.UTF8, "application/json");

            OnUpdateEmailEmail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportEmailemail_accountsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/emailemail_accounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/emailemail_accounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEmailemail_accountsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/emailemail_accounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/emailemail_accounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEmailemail_accounts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>> GetEmailemail_accounts(Query query)
        {
            return await GetEmailemail_accounts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>> GetEmailemail_accounts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Emailemail_accounts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmailemail_accounts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>>(response);
        }

        partial void OnCreateEmailEmailAccount(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> CreateEmailEmailAccount(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount emailEmailAccount = default(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount))
        {
            var uri = new Uri(baseUri, $"Emailemail_accounts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(emailEmailAccount), Encoding.UTF8, "application/json");

            OnCreateEmailEmailAccount(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>(response);
        }

        partial void OnDeleteEmailEmailAccount(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEmailEmailAccount(string emailAccountId = default(string))
        {
            var uri = new Uri(baseUri, $"Emailemail_accounts('{Uri.EscapeDataString(emailAccountId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEmailEmailAccount(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEmailEmailAccountByEmailAccountId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> GetEmailEmailAccountByEmailAccountId(string expand = default(string), string emailAccountId = default(string))
        {
            var uri = new Uri(baseUri, $"Emailemail_accounts('{Uri.EscapeDataString(emailAccountId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmailEmailAccountByEmailAccountId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>(response);
        }

        partial void OnUpdateEmailEmailAccount(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEmailEmailAccount(string emailAccountId = default(string), EspoNew.Server.Models.EspoDbNew.EmailEmailAccount emailEmailAccount = default(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount))
        {
            var uri = new Uri(baseUri, $"Emailemail_accounts('{Uri.EscapeDataString(emailAccountId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", emailEmailAccount.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(emailEmailAccount), Encoding.UTF8, "application/json");

            OnUpdateEmailEmailAccount(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportEmployeesemployeesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/employeesemployees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/employeesemployees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEmployeesemployeesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/employeesemployees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/employeesemployees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEmployeesemployees(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>> GetEmployeesemployees(Query query)
        {
            return await GetEmployeesemployees(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>> GetEmployeesemployees(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Employeesemployees");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmployeesemployees(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>>(response);
        }

        partial void OnCreateEmployeesEmployee(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> CreateEmployeesEmployee(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesEmployee = default(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee))
        {
            var uri = new Uri(baseUri, $"Employeesemployees");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(employeesEmployee), Encoding.UTF8, "application/json");

            OnCreateEmployeesEmployee(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>(response);
        }

        partial void OnDeleteEmployeesEmployee(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEmployeesEmployee(string employeeId = default(string))
        {
            var uri = new Uri(baseUri, $"Employeesemployees('{Uri.EscapeDataString(employeeId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEmployeesEmployee(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEmployeesEmployeeByEmployeeId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> GetEmployeesEmployeeByEmployeeId(string expand = default(string), string employeeId = default(string))
        {
            var uri = new Uri(baseUri, $"Employeesemployees('{Uri.EscapeDataString(employeeId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEmployeesEmployeeByEmployeeId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>(response);
        }

        partial void OnUpdateEmployeesEmployee(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEmployeesEmployee(string employeeId = default(string), EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesEmployee = default(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee))
        {
            var uri = new Uri(baseUri, $"Employeesemployees('{Uri.EscapeDataString(employeeId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", employeesEmployee.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(employeesEmployee), Encoding.UTF8, "application/json");

            OnUpdateEmployeesEmployee(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportLeadsleadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/leadsleads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/leadsleads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportLeadsleadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/leadsleads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/leadsleads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetLeadsleads(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.LeadsLead>> GetLeadsleads(Query query)
        {
            return await GetLeadsleads(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.LeadsLead>> GetLeadsleads(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Leadsleads");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLeadsleads(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.LeadsLead>>(response);
        }

        partial void OnCreateLeadsLead(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.LeadsLead> CreateLeadsLead(EspoNew.Server.Models.EspoDbNew.LeadsLead leadsLead = default(EspoNew.Server.Models.EspoDbNew.LeadsLead))
        {
            var uri = new Uri(baseUri, $"Leadsleads");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(leadsLead), Encoding.UTF8, "application/json");

            OnCreateLeadsLead(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.LeadsLead>(response);
        }

        partial void OnDeleteLeadsLead(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteLeadsLead(string leadId = default(string))
        {
            var uri = new Uri(baseUri, $"Leadsleads('{Uri.EscapeDataString(leadId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteLeadsLead(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetLeadsLeadByLeadId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.LeadsLead> GetLeadsLeadByLeadId(string expand = default(string), string leadId = default(string))
        {
            var uri = new Uri(baseUri, $"Leadsleads('{Uri.EscapeDataString(leadId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLeadsLeadByLeadId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.LeadsLead>(response);
        }

        partial void OnUpdateLeadsLead(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateLeadsLead(string leadId = default(string), EspoNew.Server.Models.EspoDbNew.LeadsLead leadsLead = default(EspoNew.Server.Models.EspoDbNew.LeadsLead))
        {
            var uri = new Uri(baseUri, $"Leadsleads('{Uri.EscapeDataString(leadId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", leadsLead.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(leadsLead), Encoding.UTF8, "application/json");

            OnUpdateLeadsLead(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMeetingsmeetingsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/meetingsmeetings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/meetingsmeetings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMeetingsmeetingsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/meetingsmeetings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/meetingsmeetings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMeetingsmeetings(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>> GetMeetingsmeetings(Query query)
        {
            return await GetMeetingsmeetings(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>> GetMeetingsmeetings(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Meetingsmeetings");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMeetingsmeetings(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>>(response);
        }

        partial void OnCreateMeetingsMeeting(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> CreateMeetingsMeeting(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsMeeting = default(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting))
        {
            var uri = new Uri(baseUri, $"Meetingsmeetings");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(meetingsMeeting), Encoding.UTF8, "application/json");

            OnCreateMeetingsMeeting(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>(response);
        }

        partial void OnDeleteMeetingsMeeting(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMeetingsMeeting(string meetingId = default(string))
        {
            var uri = new Uri(baseUri, $"Meetingsmeetings('{Uri.EscapeDataString(meetingId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMeetingsMeeting(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMeetingsMeetingByMeetingId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> GetMeetingsMeetingByMeetingId(string expand = default(string), string meetingId = default(string))
        {
            var uri = new Uri(baseUri, $"Meetingsmeetings('{Uri.EscapeDataString(meetingId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMeetingsMeetingByMeetingId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>(response);
        }

        partial void OnUpdateMeetingsMeeting(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMeetingsMeeting(string meetingId = default(string), EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsMeeting = default(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting))
        {
            var uri = new Uri(baseUri, $"Meetingsmeetings('{Uri.EscapeDataString(meetingId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", meetingsMeeting.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(meetingsMeeting), Encoding.UTF8, "application/json");

            OnUpdateMeetingsMeeting(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportOpportunitiesopportunitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/opportunitiesopportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/opportunitiesopportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportOpportunitiesopportunitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/opportunitiesopportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/opportunitiesopportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetOpportunitiesopportunities(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>> GetOpportunitiesopportunities(Query query)
        {
            return await GetOpportunitiesopportunities(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>> GetOpportunitiesopportunities(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Opportunitiesopportunities");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunitiesopportunities(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>>(response);
        }

        partial void OnCreateOpportunitiesOpportunity(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> CreateOpportunitiesOpportunity(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunity = default(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity))
        {
            var uri = new Uri(baseUri, $"Opportunitiesopportunities");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunitiesOpportunity), Encoding.UTF8, "application/json");

            OnCreateOpportunitiesOpportunity(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>(response);
        }

        partial void OnDeleteOpportunitiesOpportunity(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteOpportunitiesOpportunity(string opportunityId = default(string))
        {
            var uri = new Uri(baseUri, $"Opportunitiesopportunities('{Uri.EscapeDataString(opportunityId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOpportunitiesOpportunity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetOpportunitiesOpportunityByOpportunityId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> GetOpportunitiesOpportunityByOpportunityId(string expand = default(string), string opportunityId = default(string))
        {
            var uri = new Uri(baseUri, $"Opportunitiesopportunities('{Uri.EscapeDataString(opportunityId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunitiesOpportunityByOpportunityId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>(response);
        }

        partial void OnUpdateOpportunitiesOpportunity(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateOpportunitiesOpportunity(string opportunityId = default(string), EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunity = default(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity))
        {
            var uri = new Uri(baseUri, $"Opportunitiesopportunities('{Uri.EscapeDataString(opportunityId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", opportunitiesOpportunity.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunitiesOpportunity), Encoding.UTF8, "application/json");

            OnUpdateOpportunitiesOpportunity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTargettargetsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/targettargets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/targettargets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTargettargetsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/targettargets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/targettargets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTargettargets(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TargetTarget>> GetTargettargets(Query query)
        {
            return await GetTargettargets(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TargetTarget>> GetTargettargets(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Targettargets");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTargettargets(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TargetTarget>>(response);
        }

        partial void OnCreateTargetTarget(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTarget> CreateTargetTarget(EspoNew.Server.Models.EspoDbNew.TargetTarget targetTarget = default(EspoNew.Server.Models.EspoDbNew.TargetTarget))
        {
            var uri = new Uri(baseUri, $"Targettargets");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(targetTarget), Encoding.UTF8, "application/json");

            OnCreateTargetTarget(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.TargetTarget>(response);
        }

        partial void OnDeleteTargetTarget(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTargetTarget(string targetId = default(string))
        {
            var uri = new Uri(baseUri, $"Targettargets('{Uri.EscapeDataString(targetId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTargetTarget(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTargetTargetByTargetId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTarget> GetTargetTargetByTargetId(string expand = default(string), string targetId = default(string))
        {
            var uri = new Uri(baseUri, $"Targettargets('{Uri.EscapeDataString(targetId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTargetTargetByTargetId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.TargetTarget>(response);
        }

        partial void OnUpdateTargetTarget(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTargetTarget(string targetId = default(string), EspoNew.Server.Models.EspoDbNew.TargetTarget targetTarget = default(EspoNew.Server.Models.EspoDbNew.TargetTarget))
        {
            var uri = new Uri(baseUri, $"Targettargets('{Uri.EscapeDataString(targetId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", targetTarget.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(targetTarget), Encoding.UTF8, "application/json");

            OnUpdateTargetTarget(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTargettarget_listsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/targettarget_lists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/targettarget_lists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTargettarget_listsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/targettarget_lists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/targettarget_lists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTargettarget_lists(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TargetTargetList>> GetTargettarget_lists(Query query)
        {
            return await GetTargettarget_lists(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TargetTargetList>> GetTargettarget_lists(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Targettarget_lists");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTargettarget_lists(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TargetTargetList>>(response);
        }

        partial void OnCreateTargetTargetList(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTargetList> CreateTargetTargetList(EspoNew.Server.Models.EspoDbNew.TargetTargetList targetTargetList = default(EspoNew.Server.Models.EspoDbNew.TargetTargetList))
        {
            var uri = new Uri(baseUri, $"Targettarget_lists");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(targetTargetList), Encoding.UTF8, "application/json");

            OnCreateTargetTargetList(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.TargetTargetList>(response);
        }

        partial void OnDeleteTargetTargetList(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTargetTargetList(string targetListId = default(string))
        {
            var uri = new Uri(baseUri, $"Targettarget_lists('{Uri.EscapeDataString(targetListId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTargetTargetList(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTargetTargetListByTargetListId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.TargetTargetList> GetTargetTargetListByTargetListId(string expand = default(string), string targetListId = default(string))
        {
            var uri = new Uri(baseUri, $"Targettarget_lists('{Uri.EscapeDataString(targetListId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTargetTargetListByTargetListId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.TargetTargetList>(response);
        }

        partial void OnUpdateTargetTargetList(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTargetTargetList(string targetListId = default(string), EspoNew.Server.Models.EspoDbNew.TargetTargetList targetTargetList = default(EspoNew.Server.Models.EspoDbNew.TargetTargetList))
        {
            var uri = new Uri(baseUri, $"Targettarget_lists('{Uri.EscapeDataString(targetListId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", targetTargetList.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(targetTargetList), Encoding.UTF8, "application/json");

            OnUpdateTargetTargetList(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTaskstasksToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/taskstasks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/taskstasks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTaskstasksToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/taskstasks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/taskstasks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTaskstasks(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TasksTask>> GetTaskstasks(Query query)
        {
            return await GetTaskstasks(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TasksTask>> GetTaskstasks(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Taskstasks");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskstasks(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TasksTask>>(response);
        }

        partial void OnCreateTasksTask(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.TasksTask> CreateTasksTask(EspoNew.Server.Models.EspoDbNew.TasksTask tasksTask = default(EspoNew.Server.Models.EspoDbNew.TasksTask))
        {
            var uri = new Uri(baseUri, $"Taskstasks");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tasksTask), Encoding.UTF8, "application/json");

            OnCreateTasksTask(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.TasksTask>(response);
        }

        partial void OnDeleteTasksTask(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTasksTask(string taskId = default(string))
        {
            var uri = new Uri(baseUri, $"Taskstasks('{Uri.EscapeDataString(taskId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTasksTask(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTasksTaskByTaskId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.TasksTask> GetTasksTaskByTaskId(string expand = default(string), string taskId = default(string))
        {
            var uri = new Uri(baseUri, $"Taskstasks('{Uri.EscapeDataString(taskId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTasksTaskByTaskId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.TasksTask>(response);
        }

        partial void OnUpdateTasksTask(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTasksTask(string taskId = default(string), EspoNew.Server.Models.EspoDbNew.TasksTask tasksTask = default(EspoNew.Server.Models.EspoDbNew.TasksTask))
        {
            var uri = new Uri(baseUri, $"Taskstasks('{Uri.EscapeDataString(taskId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tasksTask.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tasksTask), Encoding.UTF8, "application/json");

            OnUpdateTasksTask(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportTeamsteamsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/teamsteams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/teamsteams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTeamsteamsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/espodbnew/teamsteams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/espodbnew/teamsteams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTeamsteams(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TeamsTeam>> GetTeamsteams(Query query)
        {
            return await GetTeamsteams(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TeamsTeam>> GetTeamsteams(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Teamsteams");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTeamsteams(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<EspoNew.Server.Models.EspoDbNew.TeamsTeam>>(response);
        }

        partial void OnCreateTeamsTeam(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.TeamsTeam> CreateTeamsTeam(EspoNew.Server.Models.EspoDbNew.TeamsTeam teamsTeam = default(EspoNew.Server.Models.EspoDbNew.TeamsTeam))
        {
            var uri = new Uri(baseUri, $"Teamsteams");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(teamsTeam), Encoding.UTF8, "application/json");

            OnCreateTeamsTeam(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.TeamsTeam>(response);
        }

        partial void OnDeleteTeamsTeam(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTeamsTeam(string teamId = default(string))
        {
            var uri = new Uri(baseUri, $"Teamsteams('{Uri.EscapeDataString(teamId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTeamsTeam(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTeamsTeamByTeamId(HttpRequestMessage requestMessage);

        public async Task<EspoNew.Server.Models.EspoDbNew.TeamsTeam> GetTeamsTeamByTeamId(string expand = default(string), string teamId = default(string))
        {
            var uri = new Uri(baseUri, $"Teamsteams('{Uri.EscapeDataString(teamId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTeamsTeamByTeamId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<EspoNew.Server.Models.EspoDbNew.TeamsTeam>(response);
        }

        partial void OnUpdateTeamsTeam(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTeamsTeam(string teamId = default(string), EspoNew.Server.Models.EspoDbNew.TeamsTeam teamsTeam = default(EspoNew.Server.Models.EspoDbNew.TeamsTeam))
        {
            var uri = new Uri(baseUri, $"Teamsteams('{Uri.EscapeDataString(teamId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", teamsTeam.ETag);    

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(teamsTeam), Encoding.UTF8, "application/json");

            OnUpdateTeamsTeam(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}