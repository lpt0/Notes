# This just runs the commands to drop the database, remove and re-create the migration, and update the database
# If multiple contexts are present, the context needs to be specified
try { Drop-Database -Context ApplicationContext } catch { Write-Host "Could not drop database: $_" }
try { Remove-Migration -Context ApplicationContext } catch { Write-Host "Could not remove migration: $_" }
try { Add-Migration -Name InitialCreate -Context ApplicationContext } catch { Write-Host "Could not add migration: $_" }
try { Update-Database -Context ApplicationContext } catch { Write-Host "Could not update database: $_" }