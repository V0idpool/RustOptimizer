# RustOptimizer

<img width="999" height="514" alt="RustOptimizer" src="https://github.com/user-attachments/assets/83beac24-7023-4642-9e07-530a14b6aeb8" />

**RustOptimizer** is a lightweight, open-source optimization tool built specifically for Rust. It tackles memory leaks, garbage collection hitches, bad `client.cfg` default settings, and Windows background overhead without sketchy "ping booster" snake oil nonsense, so you can focus on the wipe.

---

### Official Site
[https://rustoptimizer.us](https://rustoptimizer.us)

---

### Features

#### Optimization Profiles & Engine Tweaks
- **Hardware Auto-Detection:** Reads your CPU, GPU (including VRAM), and RAM to recommend the best profile for your rig.
- **4 Tuned Profiles:**
  - **Competitive (Max FPS):** Strips visuals down for the highest frame rates and lowest latency.
  - **Balanced (Good Performance & Looks):** Clean visuals paired with steady frame rates.
  - **Recommended (Optimized):** All-around sweet spot for performance and visibility.
  - **Ultra (Maximum Visuals):** True maximum shadows, water, grass, and draw distance for high-end rigs.
- **Dynamic `gc.buffer` & Incremental GC:** Automatically scales `gc.buffer` to your installed RAM (`1024`, `2048`, or `4096 MB`) so 16GB systems don't get pushed into the pagefile, and enables `gc.incremental_milliseconds 1` to smooth out any Unity GC freezes.
- **Hardware-Aware GPU & Display ConVars:** Automatically enables **NVIDIA Reflex (On + Boost)** on NVIDIA cards, plus built-in controls for Screen Mode (`Exclusive Fullscreen` / `Borderless`), In-Game FPS Cap, and Menu FPS Cap.
- **Backup, Restore & Shareable `.rop` Profiles:** Create timestamped `client.cfg` backups with one click, restore defaults anytime, or export/import custom `.rop` profiles to share setups with your group.

#### Smart RAM Auto-Flush
- **Smart Threshold Mode (Recommended):** Monitors free RAM and Standby cache, purging the Windows Standby List only when free RAM drops below your chosen GB threshold, avoiding blind mid-fight stutters.
- **Timed & Manual Flushing:** Supports fixed intervals (minutes/hours), instant flushing, and an optional flush sound effect. (For Sal :P)

#### System & Process Tweaks
- **High CPU Priority (EAC-Compatible):** Writes Windows IFEO priority (`CpuPriorityClass`) for `RustClient.exe` so Windows applies High Priority at launch before Easy Anti-Cheat locks the handle.
- **Auto High / Ultimate Power Plan Switcher:** Temporarily switches Windows to Ultimate or High Performance while Rust is running, then automatically restores your previous power plan when the game closes.
- **High-Performance GPU Binding:** Registers `RustClient.exe` with `GpuPreference=2` in Windows DXGI so laptops and iGPU setups always schedule Rust on the discrete GPU.
- **Game Mode ON & Game DVR OFF:** Keeps Windows Game Mode enabled (important for core scheduling and Ryzen X3D CCD parking) while disabling background Game DVR recording overhead.
- **EAC-Safe Background App Throttling:** Drops heavy browsers (Chrome, Edge, Firefox, Brave, Opera) and launchers to `BelowNormal` priority while Rust runs, then restores them on exit.
- **Physical Cores Only & Disable FSO:** Optional physical-core affinity mask and Windows Fullscreen Optimizations override.
- **One-Click Cache Cleaners:** Clear Steam's Rust Workshop Skin Cache (`252490`) and stale DirectX / NVIDIA / AMD GPU Shader Caches after major force-wipes or driver updates.

#### Network Tweaks & Diagnostics (No Snake Oil xD)
- **Disable NIC Power Saving / EEE:** Disables Energy-Efficient Ethernet, Green Ethernet, and NIC sleep states on active Ethernet adapters to prevent random lag spikes and rubberbanding.
- **QoS DSCP 46 Policy:** Tags `RustClient.exe` UDP traffic with Expedited Forwarding (`DSCP 46`) for QoS-aware routers.
- **Server Jitter & Packet Loss Monitor:** Paste any server IP, `IP:port`, or F1 `connect` string to measure average/min/max ping, jitter, and packet loss, plus a **Flush DNS Cache** button.

---

### How to Use

> **Back up first:** Before applying any profile, open **Profiles & Backups** and click **Save New Backup** so you can revert your `client.cfg` at any time.

**Administrator Note:** Features that modify Windows ystem policies (**RAM Auto-Flush**, **IFEO High Priority**, **NIC Power Saving**, and **QoS DSCP 46**) require running Rust Optimizer as **Administrator**.

1. **Download & Run:** Grab the latest release from [rustoptimizer.us/downloads](https://rustoptimizer.us/downloads).
2. **Set Your Game Path:** On the **Dashboard**, click **Browse...** and select your main Rust folder (e.g., `C:\Program Files (x86)\Steam\steamapps\common\Rust`).
3. **Pick & Apply a Profile:** Go to **Profiles & Backups**, select a profile card (or click **Auto-Detect**), set your Screen Mode and FPS caps, and click **Apply Profile**.
4. **Configure System, RAM & Network Tweaks:**
   - Enable desired process, power plan, GPU, and background-throttling options under **System Tweaks** and click **Save & Apply Settings**.
   - Turn on **Smart Threshold Mode** under **RAM Auto-Flush** to stop late session memory stutters.
   - Optionally disable NIC power saving, enable QoS DSCP 46, or test your server's jitter under **Network & Ping**.
5. **Launch Rust:** Click **LAUNCH RUST** at the bottom of the sidebar (or minimize to tray while you play).

*Note: If you use the DirectX/GPU Shader Cache cleaner, expect brief shader rebuild stutters during your first 5 or so minutes in-game.*

---

### Useful Links

- **Official Site:** [https://rustoptimizer.us](https://rustoptimizer.us)
- **Documentation:** [https://rustoptimizer.us/docs](https://rustoptimizer.us/docs)
- **Downloads:** [https://rustoptimizer.us/downloads](https://rustoptimizer.us/downloads)
- **Discord Support:** [https://rustoptimizer.us/invite](https://rustoptimizer.us/invite)
- **Void Tech Studios:** [https://voidtechstudios.us](https://voidtechstudios.us)
- **Donations:** [https://buymeacoffee.com/rustforgedev](https://buymeacoffee.com/rustforgedev)
