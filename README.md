# eBeertija

E - Beertija

Sustav je namijenjen voditelju, ostalim zaposlenicima, te gostima ugostiteljskih objekata za evidenciju i zaprimanje narudžbi, izdavanje računa, kreiranje cjenika, kreiranje rasporeda ugostiteljskog objekta.


TEHNOLOGIJE
Frontend: Angular CLI v 8.3.0 (Web aplikacija) 
Backend: Asp.net Api (Visual Studio 2019 + ASP.NET and web development (workload))
DBMS: postgreSql - 13.3-2
Node.js v 14.16.0


PRISTUPNI PARAMETRI ZA BAZU
Parametre je potrebno upisati unutar datoteke 'appsettings.json' koja se nalazi unutar direktorija 'eBeertijaBackend\eBeertijaBackend\appsettings.json'. U objektu "PGsqlContext" potrebno je izmijeniti atribut 'Password' - umjesto (*****) zvjezdica upisati lozinku koju ste ranije definirali prilikom instalacije postgresa.


KORACI INSTALACIJE

Visual Studio 2019 - Community - https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=16
    1. Prije instalacije VS-a, unutar VS installera potrebno je označiti [ASP.NET and web development] workload

Node.js - https://nodejs.org/en/download/

Angular CLI - Unutar CMD-a potrebno je unijeti sljedeću naredbu: npm install -g @angular/cli

Postgres - https://www.postgresql.org/download/windows/


KORACI POKRETANJA
1. Preuzeti projekt s github-a - https://github.com/mzivkovic2001/eBeertija
Unutar projekta 'ebeertija' nalaze se dva foldera: eBeertijaBackend, i eBeertijaFrontend
2. Izmijeniti pristupne parametre za spajanje na bazu (vidi: PRISTUPNI PARAMETRI ZA BAZU)
3. Otvoriti eBeertijaBackend.sln unutar visual studia:
   - Otvoriti 'Package Manager Console' - Kartica 'View' na nav. traci > Other Windows > Package Manager Console
   - unutar PMC-a unijeti naredbu 'update-database' kako bi se kreirali modeli za bazu podataka
4. Kroz cmd, pozicionirati se unutar mape 'eBeertijaFrontend' i unijeti naredbu 'npm install' kako bi se preuzeli svi plugin-ovi i paketi koje frontend projekt koristi
5. Pokretanje backenda - unutar vs-a otvoriti eBeertijaBackend.sln i unutar terminala unijeti naredbu 'dontnet run'
6. Pokretanje frontenda - putem cmd-a pozicionirati se unutar direktorija '/eBeertijaFrontend/src/app/' i unijeti naredbu 'ng serve --open'
7. Kada se unutar preglednika pojavi login screen, mogu se prijaviti tri vrste korisnika s različitim ovlastima:
   7.1. Username: voditelj, Password: voditelj
   7.2. Username: administrator, Password: administrator
   7.3. Username: konobar, Password: konobar
