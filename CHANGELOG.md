# Changelog

All notable changes to this project will be documented in this file.

## [0.5.0] - 2025-01-08
### Added
- Code Barre CRUD.

## [0.4.0] - 2025-01-08
### Added
- Chiffre d'affaires.
- Modes de paiement.

## [0.3.0] - 2025-01-07
### Added
- Catégorie CRUD functionality.
- Article => Catégorie association.

### Fixed
- Corrected relevé calculation (removed debit and credit for transferred or non-payable documents).
- Fixed stock calculation (prevented double calculation for transferred documents).
- Stock alert feature.

## [0.2.3] - 2025-01-06
### Added
- Ability to define an article as non-stockable.
- Stock status for all inputs/outputs.
- Stock sheet for all inputs/outputs.

## [0.2.2] - 2025-01-06
### Added
- Relevé for clients/suppliers with date filters.

### Fixed
- Fixed deletion of paid documents.
- Fixed CLT/FRS selection in document entry.
- Removed the "All" filter for actors.

## [0.2.1] - 2025-01-06
### Added
- Stock status report.
- Stock sheet.

### Fixed
- Fixed date format (dd/mm/yyyy).
- Fixed date entry (recorded date vs. current date).

## [0.2.0] - 2025-01-04
### Added
- User CRUD functionality.
- Stock status report.
- Stock sheet.

## [0.1.0] - 2025-01-04
### Added
- Article entry from article selection.
- Actor entry from actor selection.

### Fixed
- Prevent deletion of paid documents.
- Fixed date format (dd/mm/yyyy).
- Fixed date entry (recorded date vs. current date).