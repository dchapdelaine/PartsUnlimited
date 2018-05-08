If your PHP site is failing with some http 500 errors, try enabling logging. To do this:
- Use [[Kudu Console]] and go into `D:\home\site\wwwroot`
- In your `.user.ini` file, add  the following line (you can type `touch .user.ini` to create the file if it doesn't exist, and don't forget the starting dot on the .user.ini file!):

```
log_errors = On
```
- Restart your site
- Make a few failing requests to get some logging going.
- Now, from Kudu Console, go to `D:\home\LogFiles` and open up (or download) `php_errors.log`.

At the end, you'll find the most recent entries, which may give clues as to what's going on.