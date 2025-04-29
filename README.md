# API som lar en bruker registrere dyr
Dette APIet lar en bruker registrere dyrene sine på seg selv. Vi kan hente, opprette, oppdatere og slette brukere og dyr.
Det er brukt 1 PostgreSQL- database med 10 dupliserte schemas, og ved å bruke header ved kall så sikrer vi at kun valgte schema benyttes.

## TEKNOLOGIER
- C# .NET 8 REST-API
- Azure
- Bicep Script
- PostgreSQL Database

## ENDEPUNKTER

https://minedyrapi-web.azurewebsites.net/

Her er en oversikt over alle tilgjengelige API-endepunkter, med beskrivelse, URL, og eksempel på kall. 

**Legg alltid på header: X-Tenant-Schema: elev1** (elev1-10)

### Brukere

```GET /api/users```
Henter alle brukere med tilhørende dyr.

```GET /api/users/{id}```
Henter én bruker basert på ID med tilhørende dyr.

```POST /api/users```
Oppretter en ny bruker.

```
{
  "firstName": "Ola",
  "lastName": "Nordmann",
}
```

```PUT /api/users/{id}```
Oppdaterer eksisterende bruker med nytt fornavn og etternavn.

```
{
  "firstName": "Ola",
  "lastName": "Nordmann",
}
```

```DELETE /api/users/{id}```
Sletter en bruker og alle deres dyr.

### Dyr

```GET /api/animals```
Henter alle registrerte dyr og viser eier-id.

```GET /api/animals/{id}```
Henter ett dyr med spesifikk ID.

```POST /api/animals```
Oppretter et nytt dyr for en eksisterende bruker.

```
{
  "name": "Toto",
  "species": "Dog",
  "ownerId": 1
}
```

```PUT /api/animals/{id}```
Oppdaterer navn og art på et dyr.

```
{
  "name": "Toto II",
  "species": "Cat"
}
```

```DELETE /api/animals/{id}```
Sletter et dyr basert på ID.
