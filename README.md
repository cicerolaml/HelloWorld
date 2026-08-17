# HelloWorld – GitHub Actions / Self-Hosted Runner

## 1. Overview

This project is a simple ASP.NET Core application configured to run as a Windows Service.

The application exposes a simple HTTP endpoint:

```text
http://helloworld.loca:5000
```

The application is deployed and executed on a Windows machine using a self-hosted GitHub Actions Runner.

The purpose of this setup is to demonstrate the complete development and deployment workflow:

```text
Developer
    |
    | git push
    v
GitHub Repository
    |
    | GitHub Actions
    v
Self-Hosted Runner
    |
    v
Windows Machine
    |
    v
Windows Service
    |
    v
ASP.NET Core Application
```

---

## 2. Application

The main application entry point is:

```text
Program.cs
```

The application is configured to run as a Windows Service using:

```csharp
builder.Host.UseWindowsService();
```

The application exposes a simple `/` endpoint that returns an HTML page.

Example:

```csharp
app.MapGet("/", () => Results.Content("""
    <html>
      <body style="background-color: green;">
        <h1>Hello World!</h1>
      </body>
    </html>
    """, "text/html"));
```

The `background-color` property is intentionally simple so that changes can be easily demonstrated through the CI/CD pipeline.

---

## 3. Windows Service

The application is installed as the following Windows Service:

```text
HelloWorldApp
```

The application files are located at:

```text
C:\Users\Cicero\HelloWorld
```

The service can be started with:

```powershell
sc.exe start HelloWorldApp
```

To stop the service:

```powershell
sc.exe stop HelloWorldApp
```

Once the service is running, the application can be accessed at:

```text
http://helloworld.loca:5000
```

---

## 4. GitHub Actions Self-Hosted Runner

The GitHub Actions Runner is installed locally on the Windows machine at:

```text
C:\actions-runner
```

To start the runner manually:

```powershell
cd C:\actions-runner
.\run.cmd
```

The runner must be running in order to receive and execute GitHub Actions jobs.

The runner status can be checked in the repository:

https://github.com/cicerolaml/HelloWorld/settings/actions/runners

---

## 5. Testing a Code Change

The following example demonstrates a change to `Program.cs`.

The original application can be modified to change the page background color.

For example:

```csharp
<body style="background-color: green;">
```

This change can be committed to a feature branch and pushed to GitHub.

### Create a feature branch

Start from the latest `main` branch:

```powershell
git checkout main
git pull
git checkout -b feature/fundo-verde
```

Modify `Program.cs` and save the changes.

### Commit and push the change

```powershell
git add Program.cs
git commit -m "testa fundo verde"
git push -u origin feature/fundo-verde
```

The push sends the new branch and commit to GitHub.

---

## 6. GitHub Actions Workflow

After the branch is pushed, GitHub Actions can execute the configured workflow using the self-hosted runner.

The execution flow is:

```text
feature/fundo-verde
        |
        | git push
        v
     GitHub
        |
        | GitHub Actions
        v
Self-Hosted Runner
        |
        v
Windows Machine
        |
        v
HelloWorldApp
```

After the workflow completes, the application can be accessed through:

```text
http://helloworld.loca:5000
```

The expected result is a page displaying:

```text
Hello World!
```

with a green background.

---

## 7. Merge Feature Branch into Main

After validating the change, the feature branch can be merged into `main`.

```powershell
git checkout main
git pull
git merge feature/fundo-verde
git push
```

The updated `main` branch is then available in GitHub and can trigger the configured workflow again.

---

## 8. Summary

This setup demonstrates a complete local CI/CD workflow using:

* ASP.NET Core
* Windows Service
* Git
* GitHub
* GitHub Actions
* Self-hosted GitHub Actions Runner
* Feature branches
* Automated deployment/testing on a Windows environment
