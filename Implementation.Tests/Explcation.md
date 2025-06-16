La méthode `Dispose()` est **automatiquement appelée** par le framework de test (ici **xUnit**) à la **fin de chaque test**, si la classe de test implémente `IDisposable`.

### 🔁 Cycle de vie avec `IDisposable` dans xUnit :

Pour chaque test :

1. Une nouvelle instance de `ApiKeyStorageTests` est créée.
2. Le test (`[Fact]`) est exécuté.
3. Après le test, xUnit appelle automatiquement `Dispose()`.

---

### 🎯 Pourquoi on utilise `Dispose()` ici ?

Dans notre cas, `Dispose()` sert à **nettoyer les fichiers de test** créés dans `AppData` :

```csharp
public void Dispose()
{
    if (Directory.Exists(_appDir))
        Directory.Delete(_appDir, true);
}
```

Cela permet de :

* Éviter que les tests suivants soient influencés par les fichiers existants
* Ne pas polluer le disque avec des fichiers inutiles
* Simuler un comportement propre à chaque test (sandboxé)

---

### 🧪 Résumé :

* `Dispose()` = hook automatique post-test
* Utilisé ici pour **supprimer le dossier de configuration de test**
* Très utile pour garder des tests **isolés et fiables**

