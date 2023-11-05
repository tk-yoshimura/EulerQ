import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cm as cm

from plot_polar_util import make_polar

data = pd.read_csv("../results_disused/euler_q_complex_n4.csv", encoding="utf-8", skiprows=1)

thetas = 4096

theta, norm, y, v = data["theta"], data["norm"], data["y:=euler_q(q)"], data["v:=ln(y)*(1-q^2)"]

theta = theta.to_numpy()
norm = norm.to_numpy()
y = y.str.replace("i", "j").to_numpy().astype(np.complex128)
v = v.str.replace("i", "j").to_numpy().astype(np.complex128)

theta = theta.reshape(thetas, -1) * np.pi
norm  = norm.reshape(thetas, -1)
y  = y.reshape(thetas, -1)
v  = v.reshape(thetas, -1)

y_abs, y_phase = np.abs(y), np.angle(y)
v_abs, v_phase = np.abs(v), np.angle(v)

y_phase[y_abs == 0] = 0
v_phase[v_abs == 0] = 0

# Plot abs euler_q
ax = make_polar()

ctf = ax.pcolormesh(theta, norm, y_abs, cmap=cm.jet, vmin=0, vmax=5, shading="gouraud", rasterized=True)
colb = plt.colorbar(ctf, pad = 0.05, orientation="vertical")
colb.set_label(r"$|(q; q_\infty)|$")
colb.ax.set_ylim([0, 5])
colb.ax.set_yticks(np.arange(0, 11) / 2)
ax.grid(color="gray", alpha=0.4, linestyle="--")
 
plt.savefig("../figures/euler_q_complex_abs_plot.svg", bbox_inches="tight")

# Plot phase euler_q
ax = make_polar()

ctf = ax.pcolormesh(theta, norm, y_phase, cmap=cm.hsv, vmin=-np.pi, vmax=np.pi, shading="gouraud", rasterized=True)
colb = plt.colorbar(ctf, pad = 0.05, orientation="vertical")
colb.set_label(r"$\arg (q; q_\infty)$")
colb.ax.set_ylim([-np.pi, np.pi])
colb.ax.set_yticks(np.arange(-4, 5) * (np.pi / 4), [r"$-\pi$", r"$-3\pi/4$", r"$-\pi/2$", r"$-\pi/4$", r"$0$", r"$\pi/4$", r"$\pi/2$", r"$3\pi/4$", r"$\pi$"])
ax.grid(color="gray", alpha=0.4, linestyle="--")

plt.savefig("../figures/euler_q_complex_phase_plot.svg", bbox_inches="tight")


# Plot abs euler_q v
ax = make_polar()

ctf = ax.pcolormesh(theta, norm, v_abs, cmap=cm.jet, vmin=0, vmax=5, shading="gouraud", rasterized=True)
colb = plt.colorbar(ctf, pad = 0.05, orientation="vertical")
colb.set_label(r"$|\log(q; q_\infty) (1-q^2)|$")
colb.ax.set_ylim([0, 5])
colb.ax.set_yticks(np.arange(0, 11) / 2)
ax.grid(color="gray", alpha=0.4, linestyle="--")

plt.savefig("../figures/euler_q_complex_abs_vplot.svg", bbox_inches="tight")

# Plot phase euler_q v
ax = make_polar()

ctf = ax.pcolormesh(theta, norm, v_phase, cmap=cm.hsv, vmin=-np.pi, vmax=np.pi, shading="gouraud", rasterized=True)
colb = plt.colorbar(ctf, pad = 0.05, orientation="vertical")
colb.set_label(r"$\arg ( \log(q; q_\infty) (1-q^2) )$")
colb.ax.set_ylim([-np.pi, np.pi])
colb.ax.set_yticks(np.arange(-4, 5) * (np.pi / 4), [r"$-\pi$", r"$-3\pi/4$", r"$-\pi/2$", r"$-\pi/4$", r"$0$", r"$\pi/4$", r"$\pi/2$", r"$3\pi/4$", r"$\pi$"])
ax.grid(color="gray", alpha=0.4, linestyle="--")

plt.savefig("../figures/euler_q_complex_phase_vplot.svg", bbox_inches="tight")