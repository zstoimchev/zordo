# regarding trusting certificates

standard dotnet command on windows and mac: `dotnet dev-certs https --trust`

On linux, we need to do this manually.

### Manual Certificate Trust for Fedora
1. Export the development certificate:
    ```
    dotnet dev-certs https -ep ~/dev-cert.crt --format PEM
    ```
2. TrSince the system-ust it system-wide:
    ```
    # Copy to system certificate store
    sudo cp ~/dev-cert.crt /etc/pki/ca-trust/source/anchors/aspnet-dev-cert.crt

    # Update the certificate trust store
    sudo update-ca-trust
    ```
3. Restart your applications

something I tried using the chat
```
# 1. Export the certificate as PEM
dotnet dev-certs https --export-path ~/localhost.crt --format PEM

# 2. Install it to Fedora's trust store
sudo cp ~/localhost.crt /etc/pki/ca-trust/source/anchors/aspnet-dev-https.crt

# 3. Update the CA trust database
sudo update-ca-trust extract

# 4. Verify it was added
trust list | grep localhost
```

// some permissions were missing
```
# Fix the permissions on the certificate files
sudo chmod 644 /etc/pki/ca-trust/source/anchors/aspnet-dev-https.crt
sudo chmod 644 /etc/pki/ca-trust/source/anchors/aspnet-dev-cert.crt

# Update the trust store again
sudo update-ca-trust extract

# Verify it's trusted now
trust list | grep localhost
```




# this is new and oldest

1. Generate a developer certificate:
   ```
   dotnet dev-certs https
   ```
2. Since I am on Fedora 43, I need to trust the certificate manually:
   - Create a target directory: `mkdir -p ~/.aspnet/https`
   - Export the cert: `dotnet dev-certs https -ep ~/.aspnet/https/aspnet-dev.pfx -p devpass`
   - Convert it to PEM (Fedora trusts PEM, not PFX): 
     ```
     openssl pkcs12 -in ~/.aspnet/https/aspnet-dev.pfx -out ~/.aspnet/https/aspnet-dev.pem -nodes -nokeys
     ```
   - Trust it system-wide (Fedora / RHEL way):
      ```
     sudo cp ~/.aspnet/https/aspnet-dev.pem \ /etc/pki/ca-trust/source/anchors/aspnet-dev.crt
     
     sudo update-ca-trust
     ```
   - Verify trust: `trust list | grep -i localhost`
   - .NET trust check: `dotnet dev-certs https --check`
   - For Firefox run once:
      ```
     certutil -d sql:$HOME/.pki/nssdb -A \
     -t "C,," \
     -n "ASP.NET Core HTTPS dev cert" \
     -i ~/.aspnet/https/aspnet-dev.pem
     ```

























