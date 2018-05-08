The Kudu Console is a tool that gives you both command line and file browser access to your sites, all from the comfort of a web browser.

To access the Kudu Console, navigate to `{yoursite}.scm.azurewebsites.net`, and click on **Debug Console**.

Here is a short [screencast](http://www.youtube.com/watch?v=kxgTtIeFppk) that demonstrates the use of the Console (though it's a bit outdated).

Note that there is also a local (non-browser based) client that gives you the same Console functionality. See this [post](http://blog.amitapple.com/post/45675601255/azurewebsiteterminal) for details. The post also has some good information about the Console that also applies to the browser-based version.

Here are various things you can do with the Kudu console:

## Use it to run commands (for example, git operations)

You can use it to do most standard console operations: changing folder, copy/rename/delete files, etc.

You can use it to run arbitrary external commands. One good use case is to run git commands against your repository (if you are using git). For example, you can run 'git log', 'git status'. You can generally run arbitrary git commands, which can be useful to diagnose certain issues.

## Navigate around using the folder UI

For example, instead of typing 'cd site', you can just click on the site folder in the folder UI. The nice thing is that the console window then automatically navigates to the same folder, so the two halves of the Console are working together.

Note: by default, it only displays up to 300 items. This limit can be overridden by adding a 'maxViewItems' key and value to your browser's localStorage.

## Download files and folder

In the UI, next to each file and folder, there is a down arrow icon that lets you download the item.

For files, it directly downloads the file by navigating to it. Depending on the mime type, the browser might navigate to it (in a new Tab), or download it.

For directories, it downloads a zip file containing the full content of the folder, which is very useful to quickly download a bunch of files (either for backup or offline analysis). For example, if you go to the *Site* folder, and click the download icon for the *wwwroot* folder, you will get the full contents of your live site.

## Upload files and folder using drag and drop

You can drag files from the Windows shell (or Mac finder), and drop them directly into the Kudu Console's File Explorer UI, much like you would between two Windows shell folders. You have to try it to believe it! :)

## Upload and expand zip file

If you try to drop a zip file into the File Explorer UI (per previews section), you'll see a special zip file drop zone appearing. If you drop it there, it will copy the content of the expanded zip file, instead of the zip file itself. It's a much more efficient way to upload a large number of small files and folders (folders work best with Chrome).

This demonstrates the UI:

![kudu_drop](https://cloud.githubusercontent.com/assets/6472374/8329527/b0a2734c-1a82-11e5-97aa-949fbde67a8f.gif)

## View and edit text files

If you click the Edit icon ![pencil](https://cloud.githubusercontent.com/assets/6472374/9696655/f5c0a4ca-537c-11e5-9024-25e92a42bb2d.PNG) next to a file, the Console will open up a window where you can view and edit that file. Kudu is using Ace (https://github.com/ajaxorg/ace) for in-browser editing.
Try `CTRL + ,` for advanced features like vim key bindings. The editor is really helpful when investigating issues. You could use it to quickly tweak a `web.config` file, with syntax checking and highlighting.