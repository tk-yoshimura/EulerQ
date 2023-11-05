# EulerQ

Euler modular function note

## Definition
The Euler Q function is represented by the following infinite product, also denoted by the Q-Pochhammer symbol.  
![euler q define](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_define.svg)  

## Graph
### Numerical results on the real axis.
![euler q plot](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_plot.svg)  
![euler q logplot](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_logplot.svg)  

### Variable transformation yields a bounded curve suitable for numerical evaluation.
![euler q vplot](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_vplot.svg)  

## NumericTable
[Table(digit:300)](https://github.com/tk-yoshimura/EulerQApproximation/tree/main/results/euler_q_n32.csv)  

## NonZero LogRoot (&Phi;(q)=1)
q = -0.  
679334369989992724165789016984783579936441256412012010234918  
431856742847762651599470526827588108924759171702586718086099  
369463758389555802272035707544592279109456240352614582155450  
616435344973909866235729991887465528174068568693290066812447  
473189502504504553741300250987237061605868316567793854504189  
978430035639448527006103817728648448250002809851221265884712  
437739302756785855529694137879480078350051903905975035842033  
083686415524254448747387606693269131835790825535265787878936  
804387088060534411032905874676308304084197967426302028245044  
037141868160535979777123048756733104631979868660533830616924  
930801338945984648674439977350829241590167608706550761787998...

## MaxValue (q | max &Phi;(q))
q_max = -0.  
411248479177954773444025662435572436972040503633601105570211  
017836442913453381447150772095063339241856731081456480033459  
519730194900584872796235280441425981938799877850007894346946  
199232162562462179351528160789438258962671492753248122230456  
618610661212942585881017690448278685179049368390281841409428  
610969710907133689148278526357780142440691979016743813743352  
252789328025504742285389994624351008276745144314836961579225  
497943232385778700278841196745374319313994311736634968184193  
435040375104670922277523631959434304376996855157970358945657  
234672551713535156163704300160124548700597632973242300954367...
  
&Phi;(q_max) = 1.  
228348867038575112586878389860096824990327915769526746796298  
315103082545177832886480199936324256119640778119785800277975  
675603915212028749801283037896712222597382203127470933035372  
330162531048008378590092516435578593402681064591620502130889  
990356580002956439973063121793379466182876335977390999433307  
439811703127753282960831762226681916066105169870906927419688  
341239592282619416450353516300860064946572441110381338044510  
018369830081014227187541445541694168504972335368709033040356  
094873998958489011641170140913194320878622304051921800210335  
035857471976383959541758533602002933138606004469363730348519...

## Series
![euler q vplot series](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_vplot_series.svg)  

## Complex
### ![euler q define](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_define.svg)  
|Abs|Phase|
|---|---|
|![euler q plot complex abs](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_complex_abs_plot.svg)|![euler q plot complex phase](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_complex_phase_plot.svg)|

### ![euler q v define](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_v_define.svg)  
|Abs|Phase|
|---|---|
|![euler q vplot complex abs](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_complex_abs_vplot.svg)|![euler q vplot complex phase](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_complex_phase_vplot.svg)|

In neighborhood of |q|=1, the zero points are placed in a regular pattern.  
![euler q vplot complex abs edge](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_complex_abs_vplot_edge.svg)

## Approximation
![euler q approx](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_approx.svg)  
![euler r vplot](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/figures/euler_q_rplot.svg)  

[FP64 Source](https://github.com/tk-yoshimura/EulerQApproximation/blob/main/EulerQFP64/EulerQ.cs)  