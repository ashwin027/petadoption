Release "petadoptionsql" does not exist. Installing it now.
NAME: petadoptionsql
LAST DEPLOYED: Wed Sep  2 13:29:34 2020
NAMESPACE: default
STATUS: deployed
REVISION: 1
TEST SUITE: None
NOTES:
License Terms for MSSQL Server
Please read the License Terms Carefully.  The can be found End-User Licensing Agreement.


License Terms: https://go.microsoft.com/fwlink/?linkid=857698
Docker Hub Resource: https://hub.docker.com/r/microsoft/mssql-server-linux

Edition for this installation: Express
SQL Server Name: petadoptionsql-mssql-linux.default
Port: 1433
Collation: SQL_Latin1_General_CP1_CI_AS

Note - By Default the Express Edition is installed as part of this Chart.

Gain access to your SQL Server:
1. Get SA Password
$ printf $(kubectl get secret --namespace default petadoptionsql-mssql-linux-secret -o jsonpath="{.data.sapassword}" | base64 --decode);echo
2. You can test that SQL Server is available by the service port with with the following set of command:
(Note: You will be prompted for the SA password, use the password generated in Step 1)
$ kubectl run mssqlcli --image=microsoft/mssql-tools -ti --restart=Never --rm=true -- /bin/bash
$ sqlcmd -S petadoptionsql-mssql-linux.default,1433 -U sa
$ Password: <Enter Password for SA>

2.  Connection Library Information
https://docs.microsoft.com/en-us/sql/connect/sql-connection-libraries

3.  Connecting with JDBC
https://docs.microsoft.com/en-us/sql/connect/jdbc/connecting-to-sql-server-with-the-jdbc-driver