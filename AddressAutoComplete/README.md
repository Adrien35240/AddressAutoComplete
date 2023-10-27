# API AddressAutoComplete
>AddressAutoComplete API populates an input (country,city,address) field with relevant address suggestions.

- ## Documentation
	- [Postman Collection](AddressAutoComplete.postman_collection.json)
	- Swagger : ```https://localhost:PORT/swagger/index.html```

- ## Local deployment
	- ## Requirements
		- a osm.pbf files from  [GeoFrabrik.de](https://download.geofabrik.de/)
		- download one and add it to ./osmFiles folder, recommend to not download a big files for test (ex: [Bretagne ~280MB](https://download.geofabrik.de/europe/france/bretagne.html))
	
	- ```docker-compose up```		
- ## Roadmap
	- ### Ameliorations
		- import specific des données du fichier pbf dans postgres avec postGIS.	 
		- Indexation des données : arbres de recherche ou index spatiaux.
		- Parallélisation: paralléliser le filtrage des entités
		- Caching: mise en cache pour éviter de refaire les même recherche fréquemment.
		- Optimisation de la requéte : utiliser des méthodes plus rapides
		- Optimisation de la lecture de fichiers. 
		- Utilisation de l'API Overpass 
		- Profilage du code
		- Gestion des exceptions