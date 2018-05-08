### Deploying a Hello World Perl application

Azure Web Apps doesn't have direct support for Perl, but it's possible to get it working with some manual steps:

- Create a Web App.
- Go to [[Kudu Console]] to easily drop files into your site.
- Go to http://strawberryperl.com/releases.html and download the "32bit ZIP edition".
- Create a `perl` folder under `D:\home\site`.
- Drag and drop the perl zip file into that folder, making sure to drop it in the [special area](https://github.com/projectkudu/kudu/wiki/Kudu-console#upload-and-expand-zip-file) that expands zip files. It's a big file, so it could take several minutes.
- Go into the `D:\home\site\perl\perl\lib` folder, and create a subfolder named `FCGI`.
- On your machine, download http://search.cpan.org/CPAN/authors/id/C/CO/COSMICNET/FCGI-IIS-0.05.tar.gz
- Using a tool like 7zip, extract the file `IIS.pm` out of it (it's under `FCGI-IIS-0.05\lib\FCGI`).
- Copy that file into the FCGI folder you create above in the Kudu Console.
- Go to the [Azure Portal](https://manage.windowsazure.com/) and go to you site's Configure page.
- Under Handler Mappings, add one for: Extension: `*.pl`, Script processor path: `D:\home\site\perl\perl\bin\perl.exe`, Additional arguments: `-MFCGI::IIS=do`.

Other FastCGI operation modes for reference:

`-MFCGI::IIS=test`
This is a simple test routine, that displays a counter that increments by 1 each time 
the script is called as a FastCGI.

`-MFCGI::IIS=carp`
In this mode, `CGI::Carp qw(fatalsToBrowser)` is invoked before running the do method.

`-MFCGI::IIS=eval`
With this mode eval is used instead of the do operator. Slower run time, but allows 
you to trap errors.

`-MFCGI::IIS=evalhead`
With this mode eval is used instead of the do operator, also the `Content-Type: 
text/html` header is returned first, allowing you to trap wrong header errors.

`-MFCGI::IIS=do`
Production Mode. This is the default mode, and will be called if no arguments are given, i.e. 
`perl -MFCGI::IIS`. The calling script is loaded into the FastCGI using the do operator.

![Handler](https://cloud.githubusercontent.com/assets/6472374/10073698/7694ff64-62d3-11e5-9ec3-0cc7bab187f3.PNG)

- Using Kudu console, copy a Perl file into `D:\home\site\wwwroot`. e.g. `hello.pl`.
- Request `http://{yoursite}.azurewebsites.net/hello.pl` to run it.

Here is an example hello.pl file you can start with:

```perl
print "Content-type: text/plain\n\n";
print "Hello from Perl on Windows!\n";
```

If you're using __CGI.pm__ to write to disk (or anything that uses __File::Temp__ for that matter), be aware that Perl does not get the right temporary location in its `$ENV{TMPDIR}`. You'll need to specify it yourself at the top of your script.

Example:
```perl
BEGIN { $ENV{TMPDIR} = "d:\\local\\temp"; }

use CGI;
my $q = new CGI;
print $q->header(-type => "text/plain");

print "Temporary folder set to $ENV{TMPDIR}\n";
```


Output:

`Temporary folder set to d:\local\temp`

See this issue for more details on `File::Temp`: https://github.com/projectkudu/kudu/issues/1700

### Adding MySQL database connectivity

- Copy `D:\home\site\perl\c\bin\libmysql_.dll` to `D:\home\site\perl\perl\vendor\lib\auto\DBD\mysql`
<br>(See this [StackOverflow](http://stackoverflow.com/questions/4206439/sequenced-steps-to-install-perl-strawberry-mysql-and-dbdmysql-on-windows-xp/12723291#12723291) thread for the "Why")

- Add `BEGIN { $ENV{SYSTEMROOT} = "D:\\Windows"; }` at the top of your `.pl` script
<br>(See this [StackOverflow](https://serverfault.com/questions/639887/iis-permissions-to-allow-outgoing-rest-http-requests-from-site-code/640473) thread for the "Why")

- At this point, running your Perl script in the Kudu console should be successful.

Example:
```perl
BEGIN
{
    $ENV{TMPDIR} = "d:\\local\\temp";
    $ENV{SYSTEMROOT} = "D:\\Windows";
}

use DBI;
print "Content-type: text/plain\n\n";

$db="wordpress";
$host="a-mysql-db.cloudapp.net";
$user="db_user";
$password="db_pass";

# Connect to MySQL database
my $dbhandle = DBI->connect ("DBI:mysql:database=$db:host=$host",
                           $user,
                           $password) 
                           or die "Can't connect to database: $DBI::errstr\n";

# Execute 'SHOW TABLES'
my $statement=$dbhandle->prepare("SHOW TABLES");
$statement->execute();

while (my @row=$statement->fetchrow_array)
{
  print $row[0]."\n";
}
$statement->finish;

# Disconnect from database
$db->disconnect or warn "Disconnection error: $DBI::errstr\n";
exit;
```

```
D:\home\site\wwwroot> d:\home\site\perl\perl\bin\perl.exe TestMySQL.pl

Content-type: text/plain

wp_commentmeta
wp_comments
wp_links
wp_options
wp_postmeta
wp_posts
wp_term_relationships
wp_term_taxonomy
wp_terms
wp_usermeta
wp_users
```

- With the console test successful, accessing the script over FastCGI should now also render the expected results:


![FastCGI](https://cloud.githubusercontent.com/assets/6472374/14495855/e01f8e44-0199-11e6-9eab-809f2cb9d7ed.png)
