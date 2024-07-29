# Patch and build Jellyfin
## Jellyfin App
1. The files to be patched are:
    - `Emby.Server.Implementations/Library/LiveStreamHelper.cs` (file1)
    - `MediaBrowser.Model/Dlna/ResolutionNormalizer.cs` (file2)
2. The patch for these files are available as `resolution.patch`. The patche is built from the difference between `v10.8.9` and `v10.8.9_lbr` tags. In order to get the `diff` of two files across two brances:
    ```sh
    $ git diff <branch 1>..<branch 2> -- path/to/file
    ```
    To list only the files changed, use the option `--name-only`.
    A patch can be created simply by piping the diff to a file.
3. **Before applying the patch** check the diff of file1 and file2 between `v10.8.9` and the target branch. If the files are not different (these do not seem to often get touched), simply apply the patch `resolution.patch`:
    ```sh
    $ git apply <patch file name>
    ```
4. To sync to a newer Jellyfin tag:
    - Synchronize the `master` branch **and** the `release-*.*.z` branch between https://github.com/jellyfin/jellyfin and https://github.com/sunipkm/jellyfin.
    - In the Jellyfin remote repository ([github](https://github.com/jellyfin/jellyfin)), check the commit hash of the relevant release tag.
    - Switch to and pull the relevant release branch in local.
    - Checkout the commit hash retrieved previously.
5. To build: Follow all the procedures in the `README.MD` for the relevant branch and install the required `.NET` runtimes. In a `cmd` shell, execute
    ```cmd
    dotnet build --configuration Release
    ```
    in the root of the local `jellyfin` repository. The build files are located in `Jellyfin.Server/bin/Release/net<version>/`. Copy all the files from this directory to another directory. Retrieve the latest release of [jellyfin-ffmpeg](https://github.com/jellyfin/jellyfin-ffmpeg), and extract the files to the root of this folder.

### Note
Files can be migrated between `git` branches by:
```cmd
git checkout <branch> <path/to/file>
```

## Jellyfin Web
1. The file to be patched is `src/components/qualityOptions.js`. Follow the same procedures described for the main `jellyfin` app. The patch is provided as `file1.patch` from the diff between `v10.8.9` and `v10.8.9_lbr`.
2. Refer to documentation for building.
3. Copy the built files from `jellyfin-web/dist` directory to a directory named `jellyfin-web` in the root of the directory where all the built files were copied for `jellyfin` app and `jellyfin-ffmpeg`.

## Jellyfin Tray App
1. Update the [jellyfin-server-windows](https://github.com/jellyfin/jellyfin-server-windows) to the latest commit.
2. Follow the instructions to build the Tray App, copy the Tray App files to the directory with `jellyfin`, `jellyfin-ffmpeg` and `jellyfin-web`.
3. Build the NSIS installer.

