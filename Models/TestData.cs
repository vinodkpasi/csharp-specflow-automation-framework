using UIAutomationTests.Models;
using UIAutomationTests.Utils;
using Newtonsoft.Json;
using System.Configuration;

namespace SpecFlow.Selenium.Models
{
    public class TestData
    {
public Login Login { get; }


const string DefaultEnvironment = "dev";
public TestData()
        {
    this.Login = InitJson<Login>("login.json");
    GetLoginCredentials();

}

public static string GetEnvironment()
        {

    string environment = EnvVar.ENV ?? ConfigurationManager.AppSettings["env"] ?? DefaultEnvironment;
    return environment.ToLower();
}

void GetLoginCredentials()
        {

    Login.Username = EnvVar.UID;
    Login.Password = EnvVar.PWD;
    Util.Log.Info("uid and pwd keys values has been set");

}

string GetJsonFolderPath()
        {
    string environment = GetEnvironment();
    string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "TestData", environment);
    return jsonFolderPath;
}

T InitJson<T>(string jsonFileName)
{
    string jsonFolderPath = GetJsonFolderPath();
    string jsonFilePath = Path.Combine(jsonFolderPath, jsonFileName);
    if (!jsonFolderPath.EndsWith(DefaultEnvironment) && !File.Exists(jsonFilePath))
    {
        Util.Log.Info("Test data are using from master env");
        jsonFilePath = Path.Combine(Environment.CurrentDirectory, "TestData", DefaultEnvironment, jsonFileName);
    }
    string json = File.ReadAllText(jsonFilePath);
    return JsonConvert.DeserializeObject<T>(json);
}
    }
}
