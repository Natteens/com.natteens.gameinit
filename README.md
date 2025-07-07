# 🎮 GameInit

![Unity](https://img.shields.io/badge/Unity-2022.3+-blue.svg)
![License](https://img.shields.io/badge/License-MIT-green.svg)
![Package](https://img.shields.io/badge/Package-v1.2.1-orange.svg)

---

## 📥 Instalação

Este pacote pode ser instalado através do Unity Package Manager.

### Via Git URL
1. Abra o Package Manager (`Window` → `Package Manager`)
2. Clique no botão `+` e selecione `Add package from git URL...`
3. Digite: `https://github.com/Natteens/com.natteens.gameinit.git`

### Via Package Manager Local
1. Clone o repositório localmente
2. No Package Manager, clique no `+` e selecione `Add package from disk...`
3. Selecione o arquivo `package.json` do projeto

---

## 📋 Índice

- [🎯 Sistemas de Eventos](#-sistemas-de-eventos)
- [⏱️ Sistema de Timers](#️-sistema-de-timers)
- [🎬 Eventos de Animação](#-eventos-de-animação)
- [🛠️ Utilitários](#️-utilitários)
- [📁 Organização da Hierarquia](#-organização-da-hierarquia)
- [🎮 Exemplos de Uso](#-exemplos-de-uso)
- [📚 API Reference](#-api-reference)

---

## 🎯 Sistemas de Eventos

O GameInit oferece um sistema robusto de eventos baseado em **Event Channels** que permite comunicação desacoplada entre diferentes partes do jogo, seguindo o padrão **Observer** e **Publisher-Subscriber**.

### 📡 Event Channels Disponíveis

| Tipo | Descrição | Uso Comum |
|------|-----------|-----------|
| **VoidEventChannel** | Eventos sem parâmetros | Morte do jogador, fim de fase |
| **BoolEventChannel** | Eventos booleanos | Estados on/off, ativação |
| **IntEventChannel** | Números inteiros | Score, vida, quantidade |
| **FloatEventChannel** | Números decimais | Velocidade, tempo, percentual |
| **StringEventChannel** | Texto | Mensagens, IDs, nomes |
| **Vector2EventChannel** | Vetores 2D | Posição 2D, direção |
| **Vector3EventChannel** | Vetores 3D | Posição 3D, rotação |
| **GameEventChannel** | Eventos customizados | Eventos complexos |

### 🚀 Como Usar Event Channels

#### 1. **Criando um Event Channel**
```
📁 Project → Botão direito → Create → GameInit → Events → [Tipo do Event]
```

#### 2. **Disparando Eventos**
```csharp
// No script que dispara o evento
[SerializeField] private VoidEventChannel onPlayerDied;
[SerializeField] private IntEventChannel onScoreChanged;

void PlayerDeath()
{
    onPlayerDied.RaiseEvent();
}

void AddScore(int points)
{
    onScoreChanged.RaiseEvent(points);
}
```

#### 3. **Escutando Eventos**
```csharp
// No script que recebe o evento
[SerializeField] private VoidEventChannel onPlayerDied;
[SerializeField] private IntEventChannel onScoreChanged;

void Start()
{
    onPlayerDied.Subscribe(HandlePlayerDeath);
    onScoreChanged.Subscribe(HandleScoreChange);
}

void HandlePlayerDeath()
{
    // Lógica quando o jogador morre
    ShowGameOverScreen();
}

void HandleScoreChange(int newScore)
{
    // Atualiza UI do score
    scoreText.text = $"Score: {newScore}";
}

void OnDestroy()
{
    // Importante: desinscrever para evitar vazamentos
    onPlayerDied?.Unsubscribe(HandlePlayerDeath);
    onScoreChanged?.Unsubscribe(HandleScoreChange);
}
```

### 🎧 Event Listeners

Para uso visual no Editor, o sistema inclui **Event Listeners** prontos:

| Listener | Funcionalidade |
|----------|----------------|
| **VoidEventListener** | UnityEvents para eventos vazios |
| **BoolEventListener** | UnityEvents para eventos booleanos |
| **IntEventListener** | UnityEvents para eventos inteiros |
| **FloatEventListener** | UnityEvents para eventos decimais |
| **StringEventListener** | UnityEvents para eventos de texto |
| **Vector2EventListener** | UnityEvents para eventos Vector2 |
| **Vector3EventListener** | UnityEvents para eventos Vector3 |
| **GameEventListener** | UnityEvents para eventos customizados |

> 💡 **Dica**: Use Event Listeners quando quiser configurar respostas no Inspector sem código!

---

## ⏱️ Sistema de Timers

Sistema flexível de **timers** com diferentes tipos para diversas necessidades de gameplay.

### 🕐 Tipos de Timer

#### 1. **CountdownTimer** - Contagem Regressiva
```csharp
// Timer simples de 10 segundos
var countdown = new CountdownTimer(10f);
countdown.OnTimerStop += () => Debug.Log("Tempo esgotado!");
countdown.Start();

// Verificações úteis
if (countdown.IsFinished)
    ShowTimeUpMessage();

// Reset para novo uso
countdown.Reset();
countdown.Reset(15f); // Reset com novo tempo
```

#### 2. **StopwatchTimer** - Cronômetro
```csharp
// Cronômetro para medir tempo
var stopwatch = new StopwatchTimer();
stopwatch.Start();

// Após algum tempo...
float timeElapsed = stopwatch.Time;
Debug.Log($"Tempo decorrido: {timeElapsed:F2}s");
```

#### 3. **TickTimer** - Timer com Ticks
```csharp
// Timer infinito - tick a cada 1 segundo
var infiniteTimer = new TickTimer(1f);
infiniteTimer.OnTick += () => Debug.Log("Tick!");
infiniteTimer.Start();

// Timer com limite - 5 ticks de 2 segundos cada
var limitedTimer = new TickTimer(2f, 5);
limitedTimer.OnTick += () => Debug.Log($"Tick {limitedTimer.TickCount}/{limitedTimer.MaxTicks}");
limitedTimer.OnAllTicksCompleted += () => Debug.Log("Todos os ticks completados!");
limitedTimer.Start();

// Informações úteis
float timeUntilNext = limitedTimer.GetTimeUntilNextTick();
int remaining = limitedTimer.RemainingTicks;
float progress = limitedTimer.Progress; // 0-1 para timers com limite
```

### 🎛️ Funcionalidades dos Timers

| Funcionalidade | Descrição |
|---------------|-----------|
| **Eventos** | `OnTimerStart`, `OnTimerStop`, `OnTick`, `OnAllTicksCompleted` |
| **Controle** | `Start()`, `Stop()`, `Pause()`, `Resume()` |
| **Estado** | `IsRunning`, `IsFinished`, `IsCompleted` |
| **Progresso** | `Progress` (0-1), `Time`, `RemainingTicks` |
| **Reset** | `Reset()`, `Reset(newTime)` // CountdownTimer apenas |

### 💡 Exemplo Prático - Sistema de Power-Up
```csharp
public class PowerUpManager : MonoBehaviour
{
    private CountdownTimer powerUpTimer;
    private TickTimer damageOverTimeTimer;
    
    void Start()
    {
        // Power-up dura 30 segundos
        powerUpTimer = new CountdownTimer(30f);
        powerUpTimer.OnTimerStop += DeactivatePowerUp;
        
        // Dano a cada 0.5 segundos durante o power-up
        damageOverTimeTimer = new TickTimer(0.5f);
        damageOverTimeTimer.OnTick += DealDamage;
    }
    
    public void ActivatePowerUp()
    {
        powerUpTimer.Start();
        damageOverTimeTimer.Start();
    }
    
    void DeactivatePowerUp()
    {
        damageOverTimeTimer.Stop();
        // Lógica de desativação
    }
    
    void Update()
    {
        powerUpTimer?.Tick(Time.deltaTime);
        damageOverTimeTimer?.Tick(Time.deltaTime);
    }
}
```

---

## 🎬 Eventos de Animação

Sistema para integrar **eventos customizados** em animações do Unity de forma visual e programática.

### 🧩 Componentes

| Componente | Função |
|------------|--------|
| **AnimationEvent** | Estrutura para eventos de animação |
| **AnimationEventReceiver** | Recebe e processa eventos |
| **AnimationEventStateBehaviour** | StateMachineBehaviour para Animator |

### 🎯 Como Usar

#### 1. **Configurando o Receiver**
```csharp
public class PlayerController : MonoBehaviour
{
    private AnimationEventReceiver animReceiver;
    
    void Start()
    {
        animReceiver = GetComponent<AnimationEventReceiver>();
        
        // Adicionar eventos via código
        animReceiver.AddEvent("Jump", OnJumpEvent);
        animReceiver.AddEvent("Land", OnLandEvent);
        animReceiver.AddEvent("Attack", OnAttackEvent);
    }
    
    void OnJumpEvent()
    {
        Debug.Log("Player jumped!");
        // Spawnar partículas, tocar som, etc.
    }
    
    void OnLandEvent()
    {
        Debug.Log("Player landed!");
        // Shake camera, spawnar dust, etc.
    }
    
    void OnAttackEvent()
    {
        Debug.Log("Attack hit!");
        // Detectar colisões, aplicar dano, etc.
    }
}
```

#### 2. **Configurando na Animação**
```
1. Abra a Animation Window
2. Selecione o frame onde quer o evento
3. Clique em "Add Event"
4. Function: OnAnimationEventTriggered
5. String Parameter: "Jump" (nome do evento)
```

#### 3. **Configuração Visual no Inspector**
O `AnimationEventReceiver` também permite configurar eventos diretamente no Inspector usando UnityEvents, sem código!

---

## 🛠️ Utilitários

### 🏗️ Singleton Pattern

Implementação **thread-safe** e **otimizada** do padrão Singleton para MonoBehaviours:

```csharp
public class GameManager : Singleton<GameManager>
{
    [Header("Game Settings")]
    public float gameSpeed = 1f;
    public int maxLives = 3;
    
    protected override void Awake()
    {
        base.Awake(); // Sempre chamar primeiro!
        
        // Sua lógica de inicialização
        InitializeGame();
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = gameSpeed;
    }
}

// Uso em qualquer lugar do código
public class PlayerController : MonoBehaviour
{
    void Die()
    {
        GameManager.Instance.PauseGame();
        // Lógica de morte
    }
}
```

### ✨ Características do Singleton

- **🛡️ DontDestroyOnLoad**: Persiste entre cenas automaticamente
- **🔒 Thread-Safe**: Implementação segura para threads
- **🗑️ Auto-Cleanup**: Remove instâncias duplicadas automaticamente
- **⚡ Performance**: Acesso direto via propriedade estática

---

## 📁 Organização da Hierarquia

### 🎨 HierarchyHeader

Componente para criar **cabeçalhos visuais** e organizar melhor a hierarquia do Unity:

```csharp
// Adicione HierarchyHeader a um GameObject vazio
// Configure no Inspector:
// - Header Text: "=== MANAGERS ==="
// - Text Color: Cyan
// - Background Color: Dark Blue
// - Font Style: Bold
```

**🎨 Funcionalidades**:
- ✏️ Texto customizável
- 🎨 Cores personalizadas (texto e fundo)
- 📝 Estilos de fonte diferentes
- 📏 Separadores visuais
- 🔤 Ícones e emojis suportados

**💡 Exemplo de Organização**:
```
🎮 === GAME MANAGERS ===
    GameManager
    AudioManager
    UIManager

⚔️ === COMBAT SYSTEM ===
    EnemySpawner
    WeaponManager
    HealthSystem

🌍 === ENVIRONMENT ===
    LevelManager
    Weather
    Lighting
```

---

## 🎮 Exemplos de Uso

O pacote inclui samples completos demonstrando diferentes casos de uso:

### 🎯 2D Sample
- **📱 Sistema de eventos para UI** responsiva
- **⏰ Timers para power-ups** temporários
- **🎨 Organização de hierarquia** exemplar

### 🌟 3D Sample  
- **🎯 Sistema de eventos 3D** para interações
- **⚡ Timers para mecânicas** de gameplay
- **🎬 Eventos de animação** integrados

### 🛠️ Utilities Sample
- **📦 Prefabs pré-configurados** prontos para uso
- **📡 Event Channels** de exemplo
- **📚 Templates de código** documentados
- **🧪 Ferramentas de teste** e debug

---

## 📚 API Reference

### 📦 Namespaces Principais

```csharp
using GameInit.AnimationEvents;     // Eventos de animação
using GameInit.GameEvents.Channels; // Canais de eventos
using GameInit.GameEvents.EventListeners; // Listeners
using GameInit.Timers;              // Sistema de timers
using GameInit.Utils;               // Utilitários gerais
```

### 🔧 Métodos Principais

#### 📡 Event Channels
```csharp
// Disparar eventos
void RaiseEvent(T value)
void RaiseEvent() // Para VoidEventChannel

// Inscrições
void Subscribe(Action<T> callback)
void Unsubscribe(Action<T> callback)
void UnsubscribeAll()

// Estado
T CurrentValue { get; }
bool HasValue { get; }
int SubscriberCount { get; }
```

#### ⏱️ Timers
```csharp
// Controle
void Start()
void Stop()
void Pause()
void Resume()
void Reset()
void Reset(float newTime) // CountdownTimer apenas

// Estado
bool IsRunning { get; }
bool IsFinished { get; }
bool IsCompleted { get; } // TickTimer apenas
float Progress { get; }
float Time { get; }

// TickTimer específico
int TickCount { get; }
int MaxTicks { get; }
int RemainingTicks { get; }
float GetTickInterval()
float GetTimeUntilNextTick()
```

---

### 🐛 Reportar Bugs
Encontrou um bug? Abra uma **issue** no GitHub com:
- 📝 Descrição detalhada
- 🔄 Passos para reproduzir
- 🖥️ Versão do Unity
- 📱 Plataforma de teste

---

## 📄 Licença

Este projeto está licenciado sob a **MIT License** - consulte o arquivo [LICENSE.md](LICENSE.md) para detalhes.

---

## 📞 Suporte & Contato

### 🆘 Precisa de Ajuda?
- 📧 **Email**: [natteens.social@gmail.com](mailto:natteens.social@gmail.com)
- 🐙 **GitHub**: [https://github.com/Natteens/com.natteens.gameinit](https://github.com/Natteens/com.natteens.gameinit)
- 📖 **Documentação**: [GitHub Wiki](https://github.com/Natteens/com.natteens.gameinit/wiki)
- 💬 **Discussões**: [GitHub Discussions](https://github.com/Natteens/com.natteens.gameinit/discussions)

<p align="center">

<strong>Feito com ❤️ por <a href="https://github.com/Natteens">Nathan da Silva Miranda</a></strong><br>
<em>GameInit - Acelere o desenvolvimento dos seus jogos Unity!</em> 🚀

</p>
