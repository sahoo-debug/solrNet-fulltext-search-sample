using CommonServiceLocator;
using SolrNet;
using SolrNet.Attributes;
using SolrNet.Commands.Parameters;
using SolrNet.Impl;
using SolrNetSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolrNetSample.Services
{
    public class SolrService
    {
        //ISolrOperations<SearchResultItem> solr;
        public SolrService()
        {
            //var connection = new SolrConnection("http://localhost:8983/solr/#/SKSCore");
            //SolrNet.Startup.Init<SearchResultItem>(connection);
           // solr = ServiceLocator.Current.GetInstance<ISolrOperations<SearchResultItem>>();
        }

        public bool SaveData(ISolrOperations<SearchResultItem> solr, EmployeeModel model)
        {
            bool result;
            try
            {
                SearchResultItem objData = new SearchResultItem()
                { Id = model.Id, FirstName = model.FirstName, LastName = model.LastName, Address = model.Address, Salary = model.Salary.Value, Department = model.Department };
                solr.Add(objData);
                solr.Commit();
                result = true;
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteData(ISolrOperations<SearchResultItem> solr, string id)
        {
            bool result;
            try
            {
               solr.Delete(id);
               solr.Commit();
               result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public EmployeeModel GetEmployeeDetail(ISolrOperations<SearchResultItem> solr, string id)
        {
            EmployeeModel empmodel = null;
            SolrQueryResults<SearchResultItem> searchItems = solr.Query(new SolrQueryByField("id", id));
            if(searchItems != null)
            {
                empmodel = new EmployeeModel {
                    Id = searchItems[0].Id,
                    FirstName = searchItems[0].FirstName,
                    LastName = searchItems[0].LastName,
                    Address = searchItems[0].Address,
                    Salary = searchItems[0].Salary,
                    Department = searchItems[0].Department
                };
            }
            return empmodel;
        }
        public IList<EmployeeModel> SolrSearchData(ISolrOperations<SearchResultItem> solr, string searchValue)
        {
            IList<EmployeeModel> resultList = new List<EmployeeModel>();
            SolrQueryResults<SearchResultItem> searchItems;
            if (searchValue == null)
            {
                searchItems = solr.Query(SolrQuery.All);

            }
            else
            {
                var solrQueuries = new List<SolrQuery>();
                solrQueuries.Add(new SolrQuery("firstname_t:" + searchValue + "*"));
                solrQueuries.Add(new SolrQuery("lastname_t:" + searchValue + "*"));
                solrQueuries.Add(new SolrQuery("address_t:" + searchValue + "*"));
                solrQueuries.Add(new SolrQuery("salary_t:" + searchValue + "*"));
                solrQueuries.Add(new SolrQuery("department_t:" + searchValue + "*"));
                SolrMultipleCriteriaQuery solrQuery = new SolrMultipleCriteriaQuery(solrQueuries.ToArray(), "OR");
                searchItems = solr.Query(solrQuery);
            }
            foreach (SearchResultItem item in searchItems)
            {
                resultList.Add(new EmployeeModel { Id = item.Id, FirstName = item.FirstName, LastName = item.LastName, Address = item.Address,
                                                   Salary = item.Salary, Department = item.Department});
            }
            return resultList;
        }
    }

    public class SearchResultItem
    {
        [SolrUniqueKey("id")]      
        public string Id { get; set; }
        [SolrField("firstname_t")]
        public string FirstName { get; set; }
        [SolrField("lastname_t")]
        public string LastName { get; set; }
        [SolrField("address_t")]
        public string Address { get; set; }
        [SolrField("salary_t")]
        public decimal Salary { get; set; }
        [SolrField("department_t")]
        public string Department { get; set; }
    }
}
