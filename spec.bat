@ECHO OFF

MSBuild && nunit-console /labels bin\Debug\Mara.Drivers.WebDriver.Specs.dll

taskkill /IM nunit-agent.exe /F