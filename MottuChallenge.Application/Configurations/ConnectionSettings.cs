namespace MottuChallenge.Application.Configurations;

public class ConnectionSettings
{
    public string? MysqlConnection { get; set; }
    // Oracle connection string, used when calling stored procedures/functions in Oracle
    public string? OracleConnection { get; set; }
}