﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3
{
    public class FakeIssueRepository : IIssueRepository
    {
        public string NO_ERROR = " ";
        public string EMPTY_TITLE_ERROR = "A title is required.";
        public string EMPTY_DISCOVERY_DATETIME_ERROR = "Must select a discovery Date/TIme";
        public string FUTURE_DISOVERY_DATETIME_ERROR = "Issue can't be from the future";
        public string EMPTY_DISCOVERY_ERROR = "A discoverer is required";
        public string DUPLICATE_TITLE_ERROR = "Issue title must be unique.";

        
        private List<Issue> Issues = new List<Issue>();
        Issue IssueInUse = new Issue();

        private string ValidateIssue(Issue issue)
        {
            string title = issue.Title.Trim();
            if (title == "")
            {
                return EMPTY_TITLE_ERROR;
            }
            if (isDuplicateName(issue.Title)==true)
            {
                return DUPLICATE_TITLE_ERROR;
            }
            if (issue.DiscoveryDate == default(DateTime))
            {
                return EMPTY_DISCOVERY_DATETIME_ERROR;
            }
            if (issue.DiscoveryDate > DateTime.Now)
            {
                return FUTURE_DISOVERY_DATETIME_ERROR;
            }
            if (issue.Discoverer == null || issue.Discoverer == "")
            {
                return EMPTY_DISCOVERY_ERROR;
            }
            return NO_ERROR;
        }
        public bool isDuplicateName(string IssueTitle)
        {
            bool checker = false;
            foreach (Issue k in Issues)
            {
                if (k.Title == IssueTitle)
                    checker = true;
            }
            return checker;
        }
        public int GetNextId()
        {
            int i = 0;
            foreach (Issue k in Issues)
            {
                if (i < k.Id)
                {
                    i = k.Id;
                }
            }
            i++;
            return i;
        }
        //////////////////////////////////////////////////////////////////
        public string Add(Issue issue) {

            string validation = this.ValidateIssue(issue);
            if (validation == NO_ERROR) 
            {

                
                Issues.Add(issue);
                
                return NO_ERROR;
            }
            else
            {
                return validation;
            }
        }
        public List<Issue> GetAll(int ProjectID) {
            return Issues;
        }
        public bool Remove(Issue issue) {
            bool inList = false;

            //Sees if issue passed in is actually in the repository
            //If so, sets inList to true
            foreach (Issue i in Issues) {
                if (i.ProjectId == issue.ProjectId) {
                    inList = true;
                    break;
                }
            }

            if (inList == false) {
                return inList;
            }
            else
            {
                Issues.Remove(issue);
                return inList;
            }
        }
        public string Modify(int IssueID, Issue issue)
        {
            return "O";
        }
        public int GetTotalNumberOfIssues(int ProjectID) {

            int numberOfIssues = 0;
            foreach(Issue i in Issues) {
                if (i.ProjectId == ProjectID)
                {
                    numberOfIssues++;
                }
            }

            return numberOfIssues;
        }
        public List<string> GetIssuesByMonth(int ProjectID) {
            List<string> IssuesByMonth = new List<string>();
            //YEAR - MONTH - AMOUNT IN THAT TIME
            List<Issue> ValidIssues = new List<Issue>();
            int[] monthYearCount= new int[24];
            
            foreach (Issue i in Issues)
            {
                if (i.ProjectId == ProjectID)
                {
                    ValidIssues.Add(i);
                }
            }
           
            int yearChecker = DateTime.Today.Year;
            int tester = -1;
            foreach (Issue i in ValidIssues)
            {
                tester = yearChecker - i.DiscoveryDate.Year ;//This determines if it goes in the first 12 or second
                monthYearCount[((tester*12)+i.DiscoveryDate.Month)]++;
            }
            foreach (int i in monthYearCount)
            {
                if (monthYearCount[i] > 0)
                {
                    IssuesByMonth.Add((yearChecker-(i/12)).ToString()+ " " + ((i % 12)).ToString() + " " + monthYearCount[i]);
                }
            }
            return IssuesByMonth;
        }
        public List<string> GetIssueByDiscoverer(int ProjectID) {

            List<String> IssuesByDiscover = new List<string>();
            int[] discovererCount = new int[100];
            string[] discovererCountTracker = new string[100];
            int dct = 0;
            foreach(Issue i in Issues) {
                if (i.ProjectId == ProjectID)
                {
                    
                        foreach(string k in discovererCountTracker)
                        {
                            discovererCountTracker[dct] = i.Discoverer;
                            discovererCount[dct]++;
                            dct++;
                        }

                }
            }

            return IssuesByDiscover;
        }
        public Issue GetUserByID(int ID) {
            foreach(Issue i in Issues) {
                if (i.ProjectId == ID)
                {
                    return i;
                }
            }
            Issue k = new Issue();
            return k;
        }
    }
}
