# Bowtie Readme

Bowtie is currently being developed using Repository Design Pattern principles and Kanban as the agile framework of choice.


This project currently includes the following

1 Visual Studio solution
8 Visual Studio projects.
1 Asset folder 


==VS Projects==
===Client API===
* Bowtie.ClientREST
** API dll file for Web and REST enabled applications
===Class Library===
* Bowtie.Lib
** Main Class Libraby that is referenced in all projects
* Bowtie.Service
** Service layer for business rules and logic
* Bowtie.RepoLinq
** Data Access layer connecting to Bowtie SQL database
* Bowtie.RepoNoDB
** Data Access layer used for unit testing and simplied data
===Web Application===
* Bowtie.WebAPI
** Web API that applications communicate with to send or recieve data
* Bowtie.WebMain
** Standard website that contains both the brochure site and client game configuration functionality 
==Testing===
* Bowtie.ConsoleTest
** TEST project for Console (windows exe) based applications
* Bowtie.WebTest
** Placeholder for quick test
