<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <title>Escape The Lab — README</title>
  <link href="https://fonts.googleapis.com/css2?family=Share+Tech+Mono&family=Orbitron:wght@400;700;900&family=Exo+2:wght@300;400;600&display=swap" rel="stylesheet"/>
  <style>
    :root {
      --bg: #050a0f;
      --surface: #0a1520;
      --surface2: #0d1e2e;
      --border: #0e3a5c;
      --cyan: #00d4ff;
      --cyan-dim: #007fa3;
      --green: #00ff9d;
      --orange: #ff6b35;
      --text: #c8dde8;
      --text-dim: #5a7a8a;
      --mono: 'Share Tech Mono', monospace;
      --display: 'Orbitron', sans-serif;
      --body: 'Exo 2', sans-serif;
    }

    * { margin: 0; padding: 0; box-sizing: border-box; }

    body {
      background: var(--bg);
      color: var(--text);
      font-family: var(--body);
      font-weight: 300;
      line-height: 1.7;
      min-height: 100vh;
      overflow-x: hidden;
    }

    /* Scanline overlay */
    body::before {
      content: '';
      position: fixed;
      inset: 0;
      background: repeating-linear-gradient(
        0deg,
        transparent,
        transparent 2px,
        rgba(0,212,255,0.015) 2px,
        rgba(0,212,255,0.015) 4px
      );
      pointer-events: none;
      z-index: 999;
    }

    .container {
      max-width: 900px;
      margin: 0 auto;
      padding: 60px 32px;
    }

    /* HEADER */
    header {
      text-align: center;
      margin-bottom: 64px;
      position: relative;
    }

    .badge {
      display: inline-block;
      font-family: var(--mono);
      font-size: 11px;
      letter-spacing: 3px;
      color: var(--cyan-dim);
      border: 1px solid var(--border);
      padding: 4px 14px;
      margin-bottom: 24px;
      text-transform: uppercase;
    }

    h1 {
      font-family: var(--display);
      font-size: clamp(32px, 6vw, 56px);
      font-weight: 900;
      letter-spacing: 6px;
      text-transform: uppercase;
      color: #fff;
      text-shadow:
        0 0 20px rgba(0,212,255,0.6),
        0 0 60px rgba(0,212,255,0.2);
      line-height: 1.1;
      margin-bottom: 8px;
    }

    h1 span { color: var(--cyan); }

    .subtitle {
      font-family: var(--mono);
      font-size: 13px;
      color: var(--text-dim);
      letter-spacing: 2px;
      margin-bottom: 28px;
    }

    .desc {
      max-width: 560px;
      margin: 0 auto;
      font-size: 15px;
      color: var(--text);
      border-left: 2px solid var(--cyan-dim);
      padding-left: 16px;
      text-align: left;
    }

    .desc strong { color: var(--cyan); font-weight: 600; }

    /* DIVIDER */
    .divider {
      display: flex;
      align-items: center;
      gap: 12px;
      margin: 48px 0 32px;
    }
    .divider-line { flex: 1; height: 1px; background: var(--border); }
    .divider-label {
      font-family: var(--display);
      font-size: 10px;
      letter-spacing: 4px;
      color: var(--cyan);
      text-transform: uppercase;
    }

    /* SECTION TITLES */
    h2 {
      font-family: var(--display);
      font-size: 13px;
      letter-spacing: 4px;
      text-transform: uppercase;
      color: var(--cyan);
      margin-bottom: 16px;
    }

    h3 {
      font-family: var(--mono);
      font-size: 11px;
      letter-spacing: 3px;
      color: var(--text-dim);
      text-transform: uppercase;
      margin: 28px 0 10px;
    }

    /* SCRIPT TABLES */
    .script-group { margin-bottom: 8px; }

    table {
      width: 100%;
      border-collapse: collapse;
      font-size: 14px;
      margin-bottom: 4px;
    }

    thead tr {
      border-bottom: 1px solid var(--border);
    }

    thead th {
      font-family: var(--mono);
      font-size: 10px;
      letter-spacing: 2px;
      color: var(--text-dim);
      text-transform: uppercase;
      padding: 8px 12px;
      text-align: left;
    }

    tbody tr {
      border-bottom: 1px solid rgba(14,58,92,0.4);
      transition: background 0.15s;
    }

    tbody tr:hover { background: rgba(0,212,255,0.04); }

    tbody td {
      padding: 10px 12px;
      vertical-align: top;
    }

    .filename {
      font-family: var(--mono);
      font-size: 13px;
      color: var(--green);
      white-space: nowrap;
    }

    .desc-cell { color: var(--text); font-size: 14px; }

    /* PUZZLE FLOW */
    .flow {
      display: flex;
      flex-direction: column;
      align-items: flex-start;
      gap: 0;
      margin: 8px 0;
    }

    .flow-step {
      display: flex;
      align-items: flex-start;
      gap: 16px;
      width: 100%;
    }

    .flow-left {
      display: flex;
      flex-direction: column;
      align-items: center;
      gap: 0;
    }

    .flow-dot {
      width: 10px;
      height: 10px;
      border-radius: 50%;
      background: var(--cyan);
      box-shadow: 0 0 8px var(--cyan);
      flex-shrink: 0;
      margin-top: 6px;
    }

    .flow-line {
      width: 1px;
      flex: 1;
      min-height: 28px;
      background: linear-gradient(to bottom, var(--cyan-dim), transparent);
    }

    .flow-content {
      background: var(--surface2);
      border: 1px solid var(--border);
      border-left: 2px solid var(--cyan-dim);
      padding: 10px 16px;
      margin-bottom: 4px;
      flex: 1;
    }

    .flow-num {
      font-family: var(--mono);
      font-size: 10px;
      color: var(--cyan-dim);
      letter-spacing: 2px;
      margin-bottom: 2px;
    }

    .flow-title {
      font-family: var(--mono);
      font-size: 14px;
      color: #fff;
    }

    .flow-detail {
      font-size: 12px;
      color: var(--text-dim);
      margin-top: 2px;
    }

    .flow-exit .flow-content {
      border-left-color: var(--green);
    }

    .flow-exit .flow-dot {
      background: var(--green);
      box-shadow: 0 0 8px var(--green);
    }

    .flow-exit .flow-num { color: var(--green); }
    .flow-exit .flow-title { color: var(--green); }

    /* CONTROLS GRID */
    .controls-grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
      gap: 8px;
      margin-top: 8px;
    }

    .control-item {
      background: var(--surface2);
      border: 1px solid var(--border);
      padding: 10px 14px;
      display: flex;
      align-items: center;
      gap: 12px;
    }

    kbd {
      font-family: var(--mono);
      font-size: 11px;
      color: var(--cyan);
      background: rgba(0,212,255,0.08);
      border: 1px solid var(--cyan-dim);
      padding: 2px 8px;
      white-space: nowrap;
      flex-shrink: 0;
    }

    .control-action {
      font-size: 13px;
      color: var(--text);
    }

    /* BUILT WITH */
    .tech-list {
      display: flex;
      flex-wrap: wrap;
      gap: 8px;
      margin-top: 8px;
    }

    .tech-tag {
      font-family: var(--mono);
      font-size: 11px;
      letter-spacing: 1px;
      color: var(--orange);
      border: 1px solid rgba(255,107,53,0.3);
      background: rgba(255,107,53,0.05);
      padding: 4px 12px;
    }

    /* AUTHOR */
    .author-box {
      display: flex;
      align-items: center;
      gap: 20px;
      background: var(--surface2);
      border: 1px solid var(--border);
      padding: 20px 24px;
      margin-top: 8px;
    }

    .author-avatar {
      width: 44px;
      height: 44px;
      border-radius: 50%;
      background: linear-gradient(135deg, var(--cyan-dim), var(--border));
      display: flex;
      align-items: center;
      justify-content: center;
      font-family: var(--display);
      font-size: 16px;
      font-weight: 700;
      color: var(--cyan);
      flex-shrink: 0;
    }

    .author-name {
      font-family: var(--display);
      font-size: 14px;
      font-weight: 700;
      letter-spacing: 2px;
      color: #fff;
    }

    .author-id {
      font-family: var(--mono);
      font-size: 11px;
      color: var(--text-dim);
      letter-spacing: 1px;
      margin-top: 2px;
    }

    /* FOOTER */
    footer {
      margin-top: 64px;
      text-align: center;
      font-family: var(--mono);
      font-size: 10px;
      letter-spacing: 2px;
      color: var(--text-dim);
    }

    /* ANIMATIONS */
    @keyframes fadeUp {
      from { opacity: 0; transform: translateY(20px); }
      to   { opacity: 1; transform: translateY(0); }
    }

    header  { animation: fadeUp 0.6s ease both; }
    section { animation: fadeUp 0.6s ease both; }
    section:nth-child(2) { animation-delay: 0.1s; }
    section:nth-child(3) { animation-delay: 0.2s; }
    section:nth-child(4) { animation-delay: 0.3s; }
    section:nth-child(5) { animation-delay: 0.4s; }
    section:nth-child(6) { animation-delay: 0.5s; }
  </style>
</head>
<body>
<div class="container">

  <!-- HEADER -->
  <header>
    <div class="badge">Unity · C# · First-Person Puzzle</div>
    <h1>Escape <span>The Lab</span></h1>
    <p class="subtitle">// complete the objectives · beat the clock</p>
    <p class="desc">
      Wake up in an abandoned sci-fi facility and solve <strong>four sequential puzzles</strong>
      to escape before the <strong>30-minute countdown</strong> runs out.
      Built in Unity with a fully modular C# architecture.
    </p>
  </header>

  <!-- SCRIPTS -->
  <section>
    <div class="divider">
      <div class="divider-line"></div>
      <div class="divider-label">Scripts</div>
      <div class="divider-line"></div>
    </div>

    <div class="script-group">
      <h3>🎮 Player</h3>
      <table>
        <thead><tr><th>File</th><th>Description</th></tr></thead>
        <tbody>
          <tr><td class="filename">FPSController.cs</td><td class="desc-cell">Core player controller — movement, sprinting, jumping, crouching, head bob, zoom, and footstep audio</td></tr>
          <tr><td class="filename">PlayerDoorInteraction.cs</td><td class="desc-cell">Detects player proximity to doors, handles E key interaction and code submission</td></tr>
          <tr><td class="filename">KeyboardToUI.cs</td><td class="desc-cell">Captures keyboard input for the door code puzzle and displays it on-screen</td></tr>
        </tbody>
      </table>
    </div>

    <div class="script-group">
      <h3>🚪 Doors &amp; Puzzles</h3>
      <table>
        <thead><tr><th>File</th><th>Description</th></tr></thead>
        <tbody>
          <tr><td class="filename">DoorController.cs</td><td class="desc-cell">Handles door rotation, unlock state, player-side detection, and audio feedback</td></tr>
          <tr><td class="filename">PuzzleManager.cs</td><td class="desc-cell">Orchestrates the four puzzle sequences with sequential dependency gating</td></tr>
          <tr><td class="filename">Key.cs</td><td class="desc-cell">Collectible trigger that advances the in-game map UI on pickup</td></tr>
          <tr><td class="filename">FinishTrigger.cs</td><td class="desc-cell">Detects the player reaching the exit and triggers the win state</td></tr>
        </tbody>
      </table>
    </div>

    <div class="script-group">
      <h3>🔊 Audio &amp; UI</h3>
      <table>
        <thead><tr><th>File</th><th>Description</th></tr></thead>
        <tbody>
          <tr><td class="filename">AudioManager.cs</td><td class="desc-cell">Singleton managing all music, movement SFX, and event SFX across the game</td></tr>
          <tr><td class="filename">UIManager.cs</td><td class="desc-cell">Singleton handling panel visibility, game state transitions, and scene management</td></tr>
          <tr><td class="filename">CountdownTimer.cs</td><td class="desc-cell">30-minute countdown — triggers Game Over when reaching zero</td></tr>
          <tr><td class="filename">PopupSound.cs</td><td class="desc-cell">Plays a sound whenever a UI popup becomes active</td></tr>
        </tbody>
      </table>
    </div>

    <div class="script-group">
      <h3>✨ Visual Effects</h3>
      <table>
        <thead><tr><th>File</th><th>Description</th></tr></thead>
        <tbody>
          <tr><td class="filename">EmissionFlicker.cs</td><td class="desc-cell">Randomly flickers material emission on lights to create an unsettling atmosphere</td></tr>
          <tr><td class="filename">RotateAndBob.cs</td><td class="desc-cell">Rotates and bobs collectible items to draw player attention</td></tr>
        </tbody>
      </table>
    </div>
  </section>

  <!-- PUZZLE FLOW -->
  <section>
    <div class="divider">
      <div class="divider-line"></div>
      <div class="divider-label">Puzzle Progression</div>
      <div class="divider-line"></div>
    </div>

    <div class="flow">
      <div class="flow-step">
        <div class="flow-left"><div class="flow-dot"></div><div class="flow-line"></div></div>
        <div class="flow-content">
          <div class="flow-num">DOOR 01</div>
          <div class="flow-title">Access Code Entry</div>
          <div class="flow-detail">Type the 4-digit code via keyboard, press Return to submit</div>
        </div>
      </div>
      <div class="flow-step">
        <div class="flow-left"><div class="flow-dot"></div><div class="flow-line"></div></div>
        <div class="flow-content">
          <div class="flow-num">DOOR 02</div>
          <div class="flow-title">Stop Gas Leak</div>
          <div class="flow-detail">Hold E for 3 seconds — requires Door 01 complete</div>
        </div>
      </div>
      <div class="flow-step">
        <div class="flow-left"><div class="flow-dot"></div><div class="flow-line"></div></div>
        <div class="flow-content">
          <div class="flow-num">DOOR 03</div>
          <div class="flow-title">Activate Cryopod</div>
          <div class="flow-detail">Hold E for 4 seconds — requires Doors 01 &amp; 02 complete</div>
        </div>
      </div>
      <div class="flow-step">
        <div class="flow-left"><div class="flow-dot"></div><div class="flow-line"></div></div>
        <div class="flow-content">
          <div class="flow-num">DOOR 04</div>
          <div class="flow-title">Collect Battery</div>
          <div class="flow-detail">Auto-triggers when all previous puzzles are complete</div>
        </div>
      </div>
      <div class="flow-step flow-exit">
        <div class="flow-left"><div class="flow-dot"></div></div>
        <div class="flow-content">
          <div class="flow-num">EXIT</div>
          <div class="flow-title">Reach the exit trigger → You Win</div>
        </div>
      </div>
    </div>
  </section>

  <!-- CONTROLS -->
  <section>
    <div class="divider">
      <div class="divider-line"></div>
      <div class="divider-label">Controls</div>
      <div class="divider-line"></div>
    </div>
    <div class="controls-grid">
      <div class="control-item"><kbd>WASD</kbd><span class="control-action">Move</span></div>
      <div class="control-item"><kbd>Left Shift</kbd><span class="control-action">Sprint</span></div>
      <div class="control-item"><kbd>Left Ctrl</kbd><span class="control-action">Crouch</span></div>
      <div class="control-item"><kbd>Space</kbd><span class="control-action">Jump</span></div>
      <div class="control-item"><kbd>E</kbd><span class="control-action">Interact</span></div>
      <div class="control-item"><kbd>Return</kbd><span class="control-action">Submit door code</span></div>
      <div class="control-item"><kbd>O</kbd><span class="control-action">Toggle objectives</span></div>
      <div class="control-item"><kbd>Right Mouse</kbd><span class="control-action">Zoom</span></div>
      <div class="control-item"><kbd>Escape</kbd><span class="control-action">Toggle cursor lock</span></div>
    </div>
  </section>

  <!-- BUILT WITH -->
  <section>
    <div class="divider">
      <div class="divider-line"></div>
      <div class="divider-label">Built With</div>
      <div class="divider-line"></div>
    </div>
    <div class="tech-list">
      <span class="tech-tag">Unity</span>
      <span class="tech-tag">C#</span>
      <span class="tech-tag">Rigidbody Physics</span>
      <span class="tech-tag">Unity Canvas UI</span>
      <span class="tech-tag">AudioSource System</span>
      <span class="tech-tag">Singleton Pattern</span>
      <span class="tech-tag">Coroutines</span>
      <span class="tech-tag">Trigger Colliders</span>
    </div>
  </section>

  <!-- AUTHOR -->
  <section>
    <div class="divider">
      <div class="divider-line"></div>
      <div class="divider-label">Author</div>
      <div class="divider-line"></div>
    </div>
    <div class="author-box">
      <div class="author-avatar">MA</div>
      <div>
        <div class="author-name">Maliq Ajala</div>
      </div>
    </div>
  </section>

  <footer>
    &lt;!-- escape-the-lab · unity · c# --&gt;
  </footer>

</div>
</body>
</html>
