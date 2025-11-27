# 🏔️ Québec Adventures - Blog Participatif d'Activités

## 📋 Vue d'ensemble du projet

Application web full-stack permettant de cataloguer, partager et noter des activités au Québec et dans l'Est du Canada/États-Unis. Projet pédagogique développé composant par composant en suivant les meilleures pratiques Angular 19 et .NET 8 LTS.

## 🎯 Objectifs

- **Principal** : Créer une liste de souhaits d'activités personnalisée pour découvrir le Québec et ses environs
- **Secondaire** : Fournir un outil pratique pour les proches en visite pour explorer la région selon leurs préférences
- **Pédagogique** : Apprentissage progressif d'Angular 19, .NET 8, NgRx, RxJS et SCSS

## 👥 Contexte personnel

Français fraîchement installé à Montréal, je développe cette plateforme avec ma conjointe pour explorer notre nouvelle région et partager nos découvertes.

## ✨ Fonctionnalités principales

### Gestion des activités
- **Affichage** : Cartes visuelles présentant chaque activité
- **CRUD** : Ajout, modification et suppression d'activités (selon rôle)
- **Catégorisation** : Classification multi-critères des activités

### Système de classification
Les activités sont organisées selon 4 dimensions :

1. **Géographie** : 
   - Montréal et environs proches
   - Régions du Québec (Laurentides, Charlevoix, Cantons-de-l'Est, etc.)
   - Frontière USA (Vermont, État de New York)
   - Grandes villes de l'Est

2. **Saison** : Hiver, Printemps, Été, Automne, Toute l'année

3. **Type d'activité** :
   - Sports d'hiver (ski de randonnée, ski alpin, raquette, patin à glace)
   - Activités extérieures (randonnée, camping, kayak, trail running)
   - Gastronomie (restaurants, brunchs, marchés)
   - Culture (musées, festivals, visites urbaines)

4. **Durée** : Demi-journée, Journée complète, Week-end, Séjour (3+ jours)

### Système d'évaluation
- **Notation** : Échelle de 0 à 10
- **Coups de cœur** : Marquage des activités favorites
- **Avis et commentaires** : Retours d'expérience détaillés

### Gestion des utilisateurs
Trois niveaux de permissions :
- **Admin** : Accès complet (moi)
- **Contributeur** : Ajout/modification d'activités et commentaires (ma conjointe, proches de confiance)
- **Lecteur** : Consultation et commentaires uniquement (visiteurs, famille)

## 🗂️ Sources de données prioritaires

1. **Ski de randonnée** : [FQME - Fédération québécoise de la montagne et de l'escalade](https://fqme.qc.ca/)
2. **Activités extérieures** : [SEPAQ - Parcs nationaux du Québec](https://www.sepaq.com/)
3. **Restaurants Montréal** : [Viens Manger](https://viensmanger.com/) (prioritaire)
4. **Restaurants (complément)** : [Tastet](https://tastet.ca/)

## 🛠️ Stack technique

### Frontend
- **Framework** : Angular 19 (standalone components) [web:11]
- **State Management** : NgRx Signal Store [web:16]
- **Réactivité** : RxJS
- **Styling** : SCSS (apprentissage progressif)
- **UI Components** : Angular Material

### Backend
- **Framework** : .NET 8 LTS (support jusqu'en 2026)
- **Architecture** : Clean Architecture [web:17][web:20]
  - Domain Layer (entités, business logic)
  - Application Layer (use cases, services)
  - Infrastructure Layer (data access, externe)
  - Presentation Layer (API Controllers)
- **API** : RESTful API

### Données
- **Phase 1** : Fichiers JSON locaux (développement initial)
- **Phase 2** : Base de données (PostgreSQL)

## 📁 Architecture du projet

### Frontend - Angular 19 Structure

src/
├── app/
│ ├── core/ # Services singleton, guards, interceptors
│ │ ├── auth/
│ │ ├── guards/
│ │ └── interceptors/
│ ├── shared/ # Composants, directives, pipes réutilisables
│ │ ├── components/
│ │ ├── directives/
│ │ └── pipes/
│ ├── features/ # Modules fonctionnels (lazy-loaded)
│ │ ├── home/ # Page d'accueil (PRIORITÉ 1)
│ │ ├── activities/ # Gestion des activités
│ │ │ ├── components/
│ │ │ ├── services/
│ │ │ └── store/ # NgRx Signal Store
│ │ ├── auth/ # Authentification
│ │ └── user-profile/
│ ├── layout/ # Composants de mise en page
│ │ ├── header/
│ │ ├── footer/
│ │ └── navigation/
│ └── app.component.ts # Point d'entrée (standalone)
├── assets/
│ ├── data/ # JSON files (Phase 1)
│ ├── images/
│ └── styles/ # SCSS globaux
└── environments/

### Backend - .NET 8 Clean Architecture

QuebecAdventures/
├── QuebecAdventures.Domain/ # Entités, interfaces, business logic
│ ├── Entities/
│ │ ├── Activity.cs
│ │ ├── User.cs
│ │ ├── Review.cs
│ │ └── Category.cs
│ ├── Enums/
│ └── Interfaces/
├── QuebecAdventures.Application/ # Use cases, DTOs, services
│ ├── DTOs/
│ ├── Interfaces/
│ ├── Services/
│ └── Mappings/
├── QuebecAdventures.Infrastructure/ # Data access, external services
│ ├── Data/
│ │ ├── JsonRepository/ # Phase 1: JSON access
│ │ └── DbContext/ # Phase 2: EF Core
│ ├── Repositories/
│ └── Services/
└── QuebecAdventures.API/ # Controllers, middleware
├── Controllers/
├── Middleware/
└── Program.cs

## 🎨 Priorités de développement

### Phase 1 - Fondations visuelles (EN COURS)
1. ✅ Setup projet Angular 19 + .NET 8
2. 🚧 **App.component** : Page d'accueil élégante avec navigation
3. ⏳ Composant carte d'activité (design uniquement)
4. ⏳ Layout responsive (header/footer)

### Phase 2 - Affichage des données
- Chargement des activités depuis JSON
- Grille de cartes d'activités
- Système de filtrage basique

### Phase 3 - Interactivité
- Détail d'une activité (modal ou page)
- NgRx Signal Store pour la gestion d'état
- Navigation et routing

### Phase 4 - Authentification
- Backend : JWT authentication
- Frontend : Guards et interceptors
- Gestion des rôles (Admin/Contributeur/Lecteur)

### Phase 5 - CRUD Activités
- Formulaires d'ajout/édition (Admin/Contributeur)
- Validation côté client et serveur
- Upload d'images

### Phase 6 - Évaluations
- Système de notation (0-10)
- Coups de cœur
- Commentaires et avis

### Phase 7 - Migration base de données
- Design du schéma SQL
- Entity Framework Core
- Migration des données JSON → DB

### Phase 8 - Fonctionnalités avancées
- Recherche full-text
- Carte interactive (Google Maps/Leaflet)
- Export/partage d'itinéraires
- Statistiques personnelles

## 📚 Concepts à maîtriser (apprentissage progressif)

### Angular 19
- ✅ Standalone components (pas de NgModules) [web:11]
- Signals (réactivité native Angular)
- RxJS (Observables, operators)
- Lazy loading avec loadComponent
- Dependency injection avec inject()
- Reactive Forms
- SCSS (variables, mixins, nesting)

### NgRx Signal Store
- withState() pour l'état réactif [web:16]
- withComputed() pour les valeurs dérivées
- withMethods() pour les actions
- withEntities() pour les collections
- Integration avec RxJS [web:19]

### .NET 8
- Clean Architecture principles [web:20]
- Minimal APIs ou Controllers
- Entity Framework Core (Phase 2)
- JWT Authentication & Authorization
- Dependency Injection
- LINQ et async/await


## 📝 Notes de développement

### Approche pédagogique
- **Composant par composant** : Chaque fonctionnalité est développée et comprise avant de passer à la suivante
- **Best practices** : Code propre, typé, testé et documenté
- **Refactoring continu** : Amélioration progressive de la structure
- **Git commits atomiques** : Un commit par fonctionnalité

### Conventions de code
- **TypeScript** : Mode strict activé
- **Naming** : 
  - Components: PascalCase (HomeComponent)
  - Services: PascalCase + Service suffix (ActivityService)
  - Variables/methods: camelCase
  - Constants: UPPER_SNAKE_CASE
- **SCSS** : BEM methodology (Block__Element--Modifier)

## 🎯 MVP (Minimum Viable Product)

L'objectif minimal pour une v1.0 utilisable :
1. Page d'accueil avec navigation
2. Liste/grille d'activités avec filtres (saison, type)
3. Page détail d'une activité
4. Authentification simple (Admin/Lecteur)
5. CRUD activités (Admin uniquement)
6. Données JSON (pas de DB)

## 📌 Liens utiles

- [Angular 19 Documentation](https://angular.dev/)
- [NgRx Signal Store](https://ngrx.io/guide/signals)
- [.NET 8 Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [RxJS Documentation](https://rxjs.dev/)

---

**Version actuelle** : 0.1.0 (Setup initial)  
**Dernière mise à jour** : 27 novembre 2025  
**Prochaine étape** : Création du composant app.component avec une page d'accueil visuellement attractive
