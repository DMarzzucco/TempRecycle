# Temp Recycle

![Models](/img/logo.png)

**Temp Recycle** is a console application that scans the Windows `TEMP` folder, displaying a detailed analysis of the space occupied by files and folders. Allows you to eliminate them automatically, excluding those in use by running applications.  

This tool makes it easy to clean the system, avoiding the manual task of deleting temporary files.


## Installation 

### 1 Download the Repository

Clone the repository using the following command or manually download the code from GitHub:

```shell
git clone https://github.com/DMarzzucco/EmptyRecycleBin.NET.git
```
### 2 Copy build files

Navigate to the build folder;

```shell
cd TempRecycle
```
Copy the file `rec.exe`.

### 3 Create the installation folder

- 1. Got to Local Disk C: and create a folder named `Tools` (or any name of your choice).

- 2. Inside `Tools`, paste the `rec.exe`file.

Your directory structure should look like this:

```shell
C:\Tools\
    ├── rec.exe

```
### 4 Add the path to system environment variables

- 1. Copy the installation folder path (e.g , `C:\Tools\`).
- 2. Open the Environment Variable menu in Windows:

    - Pres `Win + R`, type `sysdm.cpl`, and press `Enter`.

    - Go to the **Advanced** tab and click **Environment Variables.**

    - Under **System Variables**, select `Path` and click **Edit**.

    - Click **New** and paste the copied path (`C:\Tools\`).

    -  Save the changes and close all windows.

### 5 Test the installation.

Open a terminal with administrator privileges and type:

```shell
rec
```
If everything is installed correctly, the application is ready to use. 

## Usage

When executing the application, it will display an analysis of the files and folders in the TEMP folder. Then, it will prompt you to confirm deletion.

### Example outPut:

![Models](/img/Example.png)

>[!CAUTION]
> - Deleting temporary files may affect running programs.
> - It is recommended to review files before deletion.
> - Some files may require elevated permissions to be deleted.


>[!TIP]
> This project is open to contributions.

## License

Made by Dario Marzzucco.