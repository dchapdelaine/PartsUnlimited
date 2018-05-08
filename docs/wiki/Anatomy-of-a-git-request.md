Kudu supports the Git Smart protocol. In summary we handle 4 requests:

## Smart Protocol

## Push
A push is made up of 2 or more requests, it's a GET followed by a 1 of more POSTs. If the first request fails for any reason, then git will try to use the **DUMB PROTOCOL** (More info below).

    GET myapp.git/info/refs?service=receive-pack

This request creates the repository if there's none there and if tells git what the server has (so it can do incremental updates).

    POST myapp.git/receive-pack  

This request sends the payload (there's may be multiple of these).

Clone is pretty much the same.

## Clone

    GET myapp.git/info/refs?service=upload-pack

    POST myapp.git/upload-pack  

This returns the response.

See for more details about the protocol http://progit.org/2010/03/04/smart-http.html.

### DUMB Protocol
We don't support this and will return a **501 Not Implemented** status code if git tries to use this we will fail. 

A 501 (Not Implemented) from Kudu generally means that a prior request failed for some reason. In previous cases where we have seen this, the initial request from the git client failed with some error (e.g. a 503), causing the git client to fall back to an unsupported protocol (hence the 501). So the 501 is not the real error, but only a side effect of the real error.
