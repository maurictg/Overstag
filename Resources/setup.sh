# Run this script before starting with development using Overstag

# PLEASE SET THESE VARIABLES FIRTST!
smtp_username=noreply@stoverstag.nl
smtp_pass= #mail password
mollie_api_token=test_TPzFzjEbPPBUFyK3T9RwKR4SfBaj22


# LIVE database (MSSQL) connection string
live_mssql_server=  #server
live_mssql_db=      #database name
live_mssql_un=      #username
live_mssql_pw=      #password

# DEBUG database (MSSQL), not in use
mssql_server=localhost
mssql_db=overstag
mssql_un=test
mssql_pw=test123

# LIVE database (MYSQL), not in use
live_mysql_server=  #server
live_mysql_db=      #database name
live_mysql_un=      #username
live_mysql_pw=      #password

# DEBUG database (MYSQL) IMPORTANT
mysql_server=localhost
mysql_db=overstag
mysql_un=test
mysql_pw=test123



# Okay thank you!

json="
{
  \"mailUsername\": \"$smtp_username\",
  \"mailPass\": \"$smtp_pass\",
  \"mollieApiToken\": \"$mollie_api_token\",
  \"msSqlConnectionString\": \"Server=$live_mssql_server;Database=$live_mssql_db;User=$live_mssql_un;Password=$live_mssql_pw;MultipleActiveResultSets=True\",
  \"mySqlConnectionString\": \"server=$mysql_server;database=$mysql_db;user=$mysql_un;password=$mysql_pw\",
  \"mySqlLiveCString\": \"Server=$live_mysql_server,Database=$live_mysql_db,Uid=$live_mysql_un,Pwd=$live_mysql_pw\",
  \"msSqlLiveCString\": \"Server=$live_mssql_server;Database=$live_mssql_db;User=$live_mssql_un;Password=$live_mssql_pw;MultipleActiveResultSets=True\",
  \"msSqlDebugCString\": \"Server=$mssql_server;database=$mssql_db;User=$mssql_un;Password=$mssql_pw\"
}
"

# Write this to the credentials file
echo $json > ../Overstag/credentials.json

echo "Credentials file written. Please run \"dotnet run\" and open \"https://localhost:5001/admin/initdb\" in your browser. This will create the database for you"
