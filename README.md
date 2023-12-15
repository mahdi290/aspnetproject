![Adminregister](https://github.com/mahdi290/aspnetproject/assets/83859113/817eb505-5685-4f67-b6d0-f0374b8b5640)
![admlogin](https://github.com/mahdi290/aspnetproject/assets/83859113/ecbecf02-d07a-4532-8aad-20129b3694d3)
![category](https://github.com/mahdi290/aspnetproject/assets/83859113/292653ec-e03d-49a1-805a-c58c6e6ae758)
![Client Hom2](https://github.com/mahdi290/aspnetproject/assets/83859113/9216c2d1-656f-4d45-a839-69a42321c58e)
![Client home1](https://github.com/mahdi290/aspnetproject/assets/83859113/304acde0-cf6a-48b8-abb3-cc9d46fc67b3)
![Client SIgnup](https://github.com/mahdi290/aspnetproject/assets/83859113/c351f666-5b8d-45bc-97f5-fe89c4358c6f)
![CommandAdmin](https://github.com/mahdi290/aspnetproject/assets/83859113/0d933472-7d40-4ae4-a032-658c542a4ba5)
![Login Client](https://github.com/mahdi290/aspnetproject/assets/83859113/0b5ac9eb-3c76-415b-8ed3-aa7c138fabce)
![Panier](https://github.com/mahdi290/aspnetproject/assets/83859113/cc97586c-8e75-4e7c-967c-459ca87ad5ed)
![Product Admin form](https://github.com/mahdi290/aspnetproject/assets/83859113/5b06057e-8b16-4090-979e-124bbc1db9fd)
![Product Admin](https://github.com/mahdi290/aspnetproject/assets/83859113/0685c34b-7372-4c0c-8a68-2c555452c63c)


1)	Introduction 
Le présent rapport documente le projet collaboratif de développement d'une application de gestion de produits utilisant ASP.NET 6 et se compose de deux parties distinctes : une interface d'administration destinée à la gestion des produits et des catégories, et une interface client permettant la sélection et l'achat de produits. 
2)	Objectif du Projet 
L'objectif principal de ce projet était de créer une application robuste, sécurisée et conviviale pour gérer un catalogue de produits. La solution a été conçue pour permettre à l'administrateur d'ajouter des catégories et des produits et consulter la liste des commandes, tandis que les clients peuvent parcourir les produits, les ajouter à leur panier, et soumettre leurs achats pour validation.
3)	Technologies Utilisées
Le projet a été développé en utilisant les dernières technologies, notamment ASP.NET 6 pour le backend, Entity Framework pour la gestion de la base de données, et Razor Pages pour la construction de l'interface utilisateur.
4)	Diagramme de classe
 
5)	Fonctionnalités Clés
Partie Admin
1.	Authentification : Mise en place d'un système d'authentification pour sécuriser l'accès à la partie d'administration.
2.	Gestion des Catégories : Permet à l'administrateur de créer de nouvelles catégories pour organiser les produits.
3.	Gestion des Produits : Possibilité d'ajouter, modifier et supprimer des produits, en les associant à des catégories existantes.
4.	Gestion des commandes : Permet à l'administrateur de consulter les commandes des clients provenant du panier.
Partie Client
1.	Authentification : Les utilisateurs doivent s'authentifier pour accéder à leur compte client.
2.	Parcours des Produits : Les clients peuvent parcourir les produits disponibles dans différentes catégories.
3.	Panier d’Achat : Ajout de produits au panier, avec la possibilité de spécifier la quantité souhaitée.
4.	Soumission d’Achat : Les clients peuvent soumettre leurs paniers d'achat pour validation.

6)	Les interfaces

Dans cette section, nous présenterons les interfaces administrateur et utilisateur de notre site web :
Admin :
Il existe cinq pages dans l'interface d'administration : la première est utilisée pour la page d'inscription, la deuxième pour la page de connexion, la troisième permet à l'administrateur de créer, de lister, de mettre à jour et de supprimer des catégories, la quatrième permet à l'administrateur d'effectuer des opérations sur le produit aussi et la dernière est utilisée pour afficher les commandes du client.

 
Figure 1 : Register Admin 

 
Figure 2 : Login Admin


 
Figure 3 : Category List
 
Figure 4 : Create Product Form
 
Figure 5 : Product List


 
Figure 6 : Orders List





Client :
L'interface client qui contient quatre interfaces utilisateurs la première est utilisée pour la page d'inscription, la deuxième est utilisée pour la page de connexion la troisième permet d'afficher à l'utilisateur les produits disponibles la dernière permet au client d'ajouter des produits au panier afin qu'il puisse les acheter plus tard ce qui affiche la somme totale des articles achetés.
 
Figure 1 : Register Client
 
Figure 2 : Login Client

   
Figure 3 : Home Page Client

 
Figure 4 : Panier



