import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.cm as cm

data = pd.read_csv("../results_disused/euler_q_complex_n4.csv", encoding="utf-8", skiprows=1)

thetas = 4096

theta, norm, v = data["theta"], data["norm"], data["v:=ln(y)*(1-q^2)"]

theta = theta.to_numpy()
norm = norm.to_numpy()
v = v.str.replace("i", "j").to_numpy().astype(np.complex128)

theta = theta.reshape(thetas, -1)
norm  = norm.reshape(thetas, -1)
v  = v.reshape(thetas, -1)

v_abs, v_phase = np.abs(v), np.angle(v)

v_phase[v_abs == 0] = 0


# Plot abs euler_q v
plt.clf()
plt.figure(figsize = (16, 4)) 

ax = plt.subplot(111)

ax.set_xlim([0, 1])
ax.set_xticks(np.arange(0, 9) / 8, [r"$0$", r"$\pi/8$", r"$\pi/4$", r"$3\pi/8$", r"$\pi/2$", r"$5\pi/8$", r"$3\pi/4$", r"$7\pi/8$", r"$\pm \pi$"])

ax.set_ylim([0.875, 1])
plt.gca().yaxis.set_major_formatter(plt.FormatStrFormatter("%.5f"))
ax.set_yticks(np.arange(28, 33) / 32)

ax.set_xlabel(r"$\arg q$")
ax.set_ylabel(r"$|q|$")

ctf = ax.pcolormesh(theta, norm, v_abs, cmap=cm.jet, vmin=0, vmax=5, shading="gouraud", rasterized=True)
colb = plt.colorbar(ctf, pad = 0.05, orientation="vertical")
colb.set_label(r"$|\log(q; q_\infty) (1-q^2)|$")
colb.ax.set_ylim([0, 5])
colb.ax.set_yticks(np.arange(0, 11) / 2)
ax.grid(color="gray", alpha=0.4, linestyle="--")

plt.savefig("../figures/euler_q_complex_abs_vplot_edge.svg", bbox_inches="tight")