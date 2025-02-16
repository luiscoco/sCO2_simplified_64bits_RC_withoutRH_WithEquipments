﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc.net;
using System.Windows.Forms;

namespace RefPropWindowsForms
{
    public partial class core
    {
        public Refrigerant working_fluid;
        public Double wmm = 0;

        public void core1(string workingfluidname, RefrigerantCategory category)
        {
            working_fluid = new Refrigerant(category, workingfluidname, ReferenceState.DEF);
            //Refrigerant working_fluid = new Refrigerant(RefrigerantCategory.NewMixture, "CO2=0.99,ARGON=0.01", ReferenceState.DEF);            
        }

        public class Compressor : core
        {
            public Double D_rotor = 0.0;      // rotor diameter (m)
            public Double D_rotor_2 = 0.0;    //secondary rotor diameter (m) [used for two-stage recompressor, if necessary]
            public Double N_design = 0.0;     //design-point shaft speed (rpm)
            public Double eta_design = 0.0;   //design-point isentropic efficiency (-) [or stage efficiency in two-stage recompressor]
            public Double phi_design = 0.0;   //design-point flow coefficient (-)
            public Double phi_min = 0.0;     //surge limit (-)
            public Double phi_max = 0.0;      //choke limit / zero pressure rise limit / x-intercept (-)
            public Double N = 0.0;            //shaft speed (rpm)
            public Double eta = 0.0;          //isentropic efficiency (-)
            public Double phi = 0.0;          //dimensionless flow coefficient (-)
            public Double phi_2 = 0.0;        //secondary dimensionless flow coefficient (-) [used for second stage phi, if necessary]
            public Double w_tip_ratio = 0.0;  //ratio of the local (comp outlet) speed of sound to the tip speed (-)
            public Boolean surge = false;       //true if the compressor is in the surge region

            public Compressor()
            {

            }
        }

        public class Turbine : core
        {
            public Double D_rotor = 0.0;     //rotor diameter (m)
            public Double A_nozzle = 0.0;    //effective nozzle area (m2)
            public Double N_design = 0.0;     //design-point shaft speed (rpm)
            public Double eta_design = 0.0;   //design-point isentropic efficiency (-)
            public Double N = 0.0;            //shaft speed (rpm)
            public Double eta = 0.0;          //isentropic efficiency (-)
            public Double nu = 0.0;           //ratio of tip speed to spouting velocity (-)
            public Double w_tip_ratio = 0.0;  //ratio of the local (turbine inlet) speed of sound to the tip speed (-)

            public Turbine()
            {

            }
        }

        public class HeatExchanger : core
        {
            //Under design conditions, streams are defined as cold (1) and hot (2)
            public Double UA_design = 0.0;                 //design-point conductance (kW/K)
            public Double DP_design1;  //design-point pressure drops across the heat exchanger (kPa)   
            public Double DP_design2;  //design-point pressure drops across the heat exchanger (kPa)
            public Double[] m_dot_design = new Double[2];  //0:Cold, 1:Hot; design-point mass flow rates of the two streams (kg/s)
            public Double Q_dot = 0.0;                       //heat transfer rate (kW)
            public Double UA = 0.0;                          //conductance (kW/K)
            public Double min_DT = 0.0;                      //minimum temperature difference in hxr (K)
            public Double eff = 0.0;                         //heat exchanger effectiveness (-)
            public Double C_dot_cold = 0.0;                  //cold stream capacitance rate (kW/K)
            public Double C_dot_hot = 0.0;                   //hot stream capacitance rate (kW/K)
            public Int64 N_sub = 1;                            //number of sub-heat exchangers used in model

            public Double T_c_in;
            public Double T_h_in;
            public Double P_c_in;
            public Double P_h_in;
            public Double P_c_out;
            public Double P_h_out;

            public HeatExchanger()
            {

            }
        }

        public class RecompCycle_withoutRH : core
        {
            public Double W_dot_net;                        //net power output of the cycle (kW)
            public Double eta_thermal;                      //thermal efficiency of the cycle (-)
            public Double recomp_frac;                      //amount of flow that bypasses the precooler and is compressed in the recompressor (-)
            public Double m_dot_turbine;                    //mass flow rate through the turbine (kg/s)
            public Double high_pressure_limit;              //maximum allowable high-side pressure (kPa)
            public Double conv_tol;                         //relative convergence tolerance used during iteration loops involving this cycle (-)
            public Turbine t = new Turbine();                 //turbine user-defined type
            //public Turbine t_rh = new Turbine();            //turbine user-defined type
            public Compressor mc = new Compressor();        //compressor and recompressor user-defined types
            public Compressor rc = new Compressor();        //compressor and recompressor user-defined types
            public HeatExchanger LT = new HeatExchanger();  //heat exchanger Low Temperature Recuperator
            public HeatExchanger HT = new HeatExchanger();  //heat exchanger High Temperature Recuperator
            public HeatExchanger PHX = new HeatExchanger();  //heat exchanger Primary Heat Exchanger
            //public HeatExchanger RHX = new HeatExchanger();  //heat exchanger ReHeating Heat Exchanger
            public HeatExchanger PC = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public Double[] temp = new Double[10];          //thermodynamic properties at the state points of the cycle (K, kPa, kJ/kg, kJ/kg-K, kg/m3)
            public Double[] pres = new Double[10];
            public Double[] enth = new Double[10];
            public Double[] entr = new Double[10];
            public Double[] dens = new Double[10];

            public RecompCycle_withoutRH()
            {

            }
        }

        public class RecompCycle : core
        {
            public Double W_dot_net;                        //net power output of the cycle (kW)
            public Double eta_thermal;                      //thermal efficiency of the cycle (-)
            public Double recomp_frac;                      //amount of flow that bypasses the precooler and is compressed in the recompressor (-)
            public Double m_dot_turbine;                    //mass flow rate through the turbine (kg/s)
            public Double high_pressure_limit;              //maximum allowable high-side pressure (kPa)
            public Double conv_tol;                         //relative convergence tolerance used during iteration loops involving this cycle (-)
            public Turbine t = new Turbine();                 //turbine user-defined type
            public Turbine t_rh = new Turbine();            //turbine user-defined type
            public Turbine p_rh = new Turbine();            //turbine user-defined type
            public Compressor mc = new Compressor();        //compressor and recompressor user-defined types
            public Compressor rc = new Compressor();        //compressor and recompressor user-defined types
            public HeatExchanger LT = new HeatExchanger();  //heat exchanger Low Temperature Recuperator
            public HeatExchanger HT = new HeatExchanger();  //heat exchanger High Temperature Recuperator
            public HeatExchanger PHX = new HeatExchanger();  //heat exchanger Primary Heat Exchanger
            public HeatExchanger RHX = new HeatExchanger();  //heat exchanger ReHeating Heat Exchanger
            public HeatExchanger PC = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public Double[] temp = new Double[12];          //thermodynamic properties at the state points of the cycle (K, kPa, kJ/kg, kJ/kg-K, kg/m3)
            public Double[] pres = new Double[12];
            public Double[] enth = new Double[12];
            public Double[] entr = new Double[12];
            public Double[] dens = new Double[12];

            public RecompCycle()
            {

            }
        }

        public class PCRCwithReheating : core
        {
            public Double W_dot_net;                        //net power output of the cycle (kW)
            public Double eta_thermal;                      //thermal efficiency of the cycle (-)
            public Double recomp_frac;                      //amount of flow that bypasses the precooler and is compressed in the recompressor (-)
            public Double m_dot_turbine;                    //mass flow rate through the turbine (kg/s)
            public Double high_pressure_limit;              //maximum allowable high-side pressure (kPa)
            public Double conv_tol;                         //relative convergence tolerance used during iteration loops involving this cycle (-)
            public Turbine t = new Turbine();                 //turbine user-defined type
            public Turbine t_rh = new Turbine();            //turbine user-defined type
            public Turbine p_rh = new Turbine();            //turbine user-defined type
            public Compressor mc = new Compressor();        //compressor and recompressor user-defined types
            public Compressor pc = new Compressor();        //compressor and pre-compressor user-defined types
            public Compressor rc = new Compressor();        //compressor and recompressor user-defined types
            public HeatExchanger LT = new HeatExchanger();  //heat exchanger Low Temperature Recuperator
            public HeatExchanger HT = new HeatExchanger();  //heat exchanger High Temperature Recuperator
            public HeatExchanger PHX = new HeatExchanger();  //heat exchanger Primary Heat Exchanger
            public HeatExchanger RHX = new HeatExchanger();  //heat exchanger ReHeating Heat Exchanger
            public HeatExchanger PC = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public HeatExchanger COOLER = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public Double[] temp = new Double[14];          //thermodynamic properties at the state points of the cycle (K, kPa, kJ/kg, kJ/kg-K, kg/m3)
            public Double[] pres = new Double[14];
            public Double[] enth = new Double[14];
            public Double[] entr = new Double[14];
            public Double[] dens = new Double[14];

            public PCRCwithReheating()
            {

            }
        }

        public class PCRCwithoutReheating : core
        {
            public Double W_dot_net;                        //net power output of the cycle (kW)
            public Double eta_thermal;                      //thermal efficiency of the cycle (-)
            public Double recomp_frac;                      //amount of flow that bypasses the precooler and is compressed in the recompressor (-)
            public Double m_dot_turbine;                    //mass flow rate through the turbine (kg/s)
            public Double high_pressure_limit;              //maximum allowable high-side pressure (kPa)
            public Double conv_tol;                         //relative convergence tolerance used during iteration loops involving this cycle (-)
            public Turbine t = new Turbine();                 //turbine user-defined type
            public Compressor mc = new Compressor();        //compressor and recompressor user-defined types
            public Compressor pc = new Compressor();        //compressor and pre-compressor user-defined types
            public Compressor rc = new Compressor();        //compressor and recompressor user-defined types
            public HeatExchanger LT = new HeatExchanger();  //heat exchanger Low Temperature Recuperator
            public HeatExchanger HT = new HeatExchanger();  //heat exchanger High Temperature Recuperator
            public HeatExchanger PHX = new HeatExchanger();  //heat exchanger Primary Heat Exchange
            public HeatExchanger PC = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public HeatExchanger COOLER = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public Double[] temp = new Double[12];          //thermodynamic properties at the state points of the cycle (K, kPa, kJ/kg, kJ/kg-K, kg/m3)
            public Double[] pres = new Double[12];
            public Double[] enth = new Double[12];
            public Double[] entr = new Double[12];
            public Double[] dens = new Double[12];

            public PCRCwithoutReheating()
            {

            }
        }

        public class RCMCIwithoutReheating : core
        {
            public Double W_dot_net;                        //net power output of the cycle (kW)
            public Double eta_thermal;                      //thermal efficiency of the cycle (-)
            public Double recomp_frac;                      //amount of flow that bypasses the precooler and is compressed in the recompressor (-)
            public Double m_dot_turbine;                    //mass flow rate through the turbine (kg/s)
            public Double high_pressure_limit;              //maximum allowable high-side pressure (kPa)
            public Double conv_tol;                         //relative convergence tolerance used during iteration loops involving this cycle (-)
            public Turbine t = new Turbine();                 //turbine user-defined type
            public Compressor mc1 = new Compressor();        //compressor and recompressor user-defined types
            public Compressor mc2 = new Compressor();        //compressor and pre-compressor user-defined types
            public Compressor rc = new Compressor();        //compressor and recompressor user-defined types
            public HeatExchanger LT = new HeatExchanger();  //heat exchanger Low Temperature Recuperator
            public HeatExchanger HT = new HeatExchanger();  //heat exchanger High Temperature Recuperator
            public HeatExchanger PHX = new HeatExchanger();  //heat exchanger Primary Heat Exchange
            public HeatExchanger PC = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public HeatExchanger COOLER = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public Double[] temp = new Double[12];          //thermodynamic properties at the state points of the cycle (K, kPa, kJ/kg, kJ/kg-K, kg/m3)
            public Double[] pres = new Double[12];
            public Double[] enth = new Double[12];
            public Double[] entr = new Double[12];
            public Double[] dens = new Double[12];

            public RCMCIwithoutReheating()
            {

            }
        }

        public class RCMCIwithReheating : core
        {
            public Double W_dot_net;                        //net power output of the cycle (kW)
            public Double eta_thermal;                      //thermal efficiency of the cycle (-)
            public Double recomp_frac;                      //amount of flow that bypasses the precooler and is compressed in the recompressor (-)
            public Double m_dot_turbine;                    //mass flow rate through the turbine (kg/s)
            public Double high_pressure_limit;              //maximum allowable high-side pressure (kPa)
            public Double conv_tol;                         //relative convergence tolerance used during iteration loops involving this cycle (-)
            public Turbine t = new Turbine();                 //turbine user-defined type
            public Turbine trh = new Turbine();                 //turbine user-defined type
            public Compressor mc1 = new Compressor();        //compressor and recompressor user-defined types
            public Compressor mc2 = new Compressor();        //compressor and pre-compressor user-defined types
            public Compressor rc = new Compressor();        //compressor and recompressor user-defined types
            public HeatExchanger LT = new HeatExchanger();  //heat exchanger Low Temperature Recuperator
            public HeatExchanger HT = new HeatExchanger();  //heat exchanger High Temperature Recuperator
            public HeatExchanger PHX = new HeatExchanger();  //heat exchanger Primary Heat Exchange
            public HeatExchanger RHX = new HeatExchanger();  //heat exchanger Reheating Heat Exchange
            public HeatExchanger PC = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public HeatExchanger COOLER = new HeatExchanger();   //heat exchanger Air Cooling Heat Exchanger
            public Double[] temp = new Double[14];          //thermodynamic properties at the state points of the cycle (K, kPa, kJ/kg, kJ/kg-K, kg/m3)
            public Double[] pres = new Double[14];
            public Double[] enth = new Double[14];
            public Double[] entr = new Double[14];
            public Double[] dens = new Double[14];

            public RCMCIwithReheating()
            {

            }
        }

        public class ErrorTrace : core
        {
            Int64 code = 0;     // the generated error code
            Int64[] lines = new Int64[4];  // the lines of the calls that generated the error (warning: these are hard-coded and need to be updated if file changes)
            Int64[] files = new Int64[4];  // the files of the calls that generated the error, using:
                                           //1: core, 2: design_point, 3: off_design_point, 4: compressors, 5: turbines, 6: heat_exchangers, 7+: user-defined
        }

        //Function for calculating turbomachines outlet conditions given the inlet conditions and the efficiency
        public void calculate_turbomachine_outlet(core luis, Double T_in, Double P_in, Double P_out, Double eta, Boolean is_comp, ref Int64 error_code, ref Double enth_in, ref Double entr_in, ref Double dens_in, ref Double temp_out, ref Double enth_out, ref Double entr_out, ref Double dens_out, ref Double spec_work)
        {
            wmm = luis.working_fluid.MolecularWeight;

            // Determine the outlet state of a compressor or turbine using isentropic efficiency and outlet pressure.

            // Inputs:
            //   T_in -- inlet temperature (K)
            //   P_in -- inlet pressure (kPa)
            //   P_out -- outlet pressure (kPa)
            //   eta -- isentropic efficiency (-)
            //   is_comp -- if .true., model a compressor (w = w_s / eta); if .false., model a turbine (w = w_s * eta)

            // Outputs:
            //   error_trace -- an ErrorTrace object
            //   enth_in -- inlet specific enthalpy (kJ/kg) [optional]
            //   entr_in -- inlet specific entropy (kJ/kg-K) [optional]
            //   dens_in -- inlet fluid density (kg/m3) [optional]
            //   temp_out -- outlet fluid temperature (K) [optional]
            //   enth_out -- outlet specific enthalpy (kJ/kg) [optional]
            //   entr_out -- outlet specific entropy (kJ/kg-K) [optional]
            //   dens_out -- outlet fluid density (kg/m3) [optional]
            //   spec_work -- specific work of the turbomachine (kJ/kg) [optional]

            // Notes:
            //   1) The specific work of the turbomachine is positive for a turbine and negative for a compressor.
            //   2) No error checking is performed on the inlet and outlet pressures; valid pressure ratios are assumed.

            //Local Variables
            Double w_s, w, h_s_out_mol, h_s_out;
            Double enth_in_mol, entr_in_mol, dens_in_mol, entr_out_mol, dens_out_mol;
            //Int64 error_code;

            //This funcitions call TP, PS and PH to calculate working_fluid states:

            // Calculate rest of properties at the INLET CONDITIONS
            //Function TP: inputs (Temperature (K) and Pressure(kPa)); outputs (enth=h_in, entr=s_in, dens=dens_in)
            working_fluid.FindStateWithTP(T_in, P_in);
            enth_in = working_fluid.Enthalpy;
            entr_in = working_fluid.Entropy;
            dens_in = working_fluid.Density;
            //if (working_fluid.ierr!= 0) 
            //{
            //    MessageBox.Show("Error calculating the INLET CONDITIONS, calling working fluid TP function in Core.cs file in the function 'calculate_turbomachine_outlet'");
            //  return;
            //}

            //Calculates OUTLET CONDITIONS: Enthalpy if compression/expansion is isentropic
            //Function PS: inputs (Pressure (kPa) and Entropy(J/mol K)); outputs (enth=h_s_out)
            entr_in_mol = entr_in * wmm;
            working_fluid.FindStatueWithPS(P_out, entr_in_mol);
            h_s_out = working_fluid.Enthalpy;

            w_s = enth_in - h_s_out;  // specific work if process is isentropic (negative for compression, positive for expansion)



            if (is_comp)
            {
                w = w_s / eta;            // actual specific work of compressor (negative value)
            }
            else
            {
                w = w_s * eta;            // actual specific work of turbine (positive value)
            }

            enth_out = enth_in - w;   // energy balance on turbomachine

            //Calculate properties at OUTLET CONDITIONS
            //Function PH: inputs (Pressure (kPa) and Enthalpy (J/mol)); outputs (temp=temp_out, entr=entr_out, dens=dens_out)
            working_fluid.FindStatueWithPH(P_out, enth_out * wmm);
            temp_out = working_fluid.Temperature;
            entr_out = working_fluid.Entropy;
            dens_out = working_fluid.Density;
            spec_work = w;
        }

        //Function for Calculating the Polytrophic efficienccy in the Turbomachines
        public void isen_eta_from_poly_eta(core luis, Double T_in, Double P_in, Double P_out, Double poly_eta, Boolean is_comp, ref Int64 error_code, ref Double isen_eta)
        {
            wmm = luis.working_fluid.MolecularWeight;

            //Calculate the isentropic efficiency that corresponds to a given polytropic efficiency
            //for the expansion or compression from T_in and P_in to P_out.
            //
            // Inputs:
            //   T_in -- inlet temperature (K)
            //   P_in -- inlet pressure (kPa)
            //   P_out -- outlet pressure (kPa)
            //   poly_eta -- polytropic efficiency (-)
            //   is_comp -- if .true., model a compressor (w = w_s / eta); if .false., model a turbine (w = w_s * eta)
            //
            // Outputs:
            //   error_trace -- an ErrorTrace object
            //   isen_eta -- the equivalent isentropic efficiency (-)
            //
            // Notes:
            //   1) Integration of small DP is approximated numerically by using 200 stages.
            //   2) No error checking is performed on the inlet and outlet pressures; valid pressure ratios are assumed.


            // Parameters
            Int64 stages = 200;

            // Local Variables
            Double h_in, s_in, h_s_out, w_s, w, stage_DP;
            Double stage_P_in, stage_P_out, stage_h_in, stage_s_in, stage_h_s_out;
            Double stage_h_out = 0;
            Int64 stage;

            working_fluid.FindStateWithTP(T_in, P_in); // properties at the inlet conditions
            h_in = working_fluid.Enthalpy;
            s_in = working_fluid.Entropy;

            working_fluid.FindStatueWithPS(P_out, s_in * wmm);  // outlet enthalpy if compression/expansion is isentropic
            h_s_out = working_fluid.Enthalpy;

            stage_P_in = P_in;   // initialize first stage inlet pressure
            stage_h_in = h_in;   // initialize first stage inlet enthalpy
            stage_s_in = s_in;   // initialize first stage inlet entropy
            stage_DP = (P_out - P_in) / Convert.ToDouble(stages);  // pressure change per stage

            for (stage = 1; stage < stages; stage++)
            {
                stage_P_out = stage_P_in + stage_DP;

                //Calculate outlet enthalpy if compression/expansion is isentropic
                working_fluid.FindStatueWithPS(stage_P_out, stage_s_in * wmm);
                stage_h_s_out = working_fluid.Enthalpy;

                w_s = stage_h_in - stage_h_s_out;  // specific work if process is isentropic

                if (is_comp == true)
                {
                    w = w_s / poly_eta;            // actual specific work of compressor (negative value)
                }

                else
                {
                    w = w_s * poly_eta;            // actual specific work of turbine (positive value)
                }

                stage_h_out = stage_h_in - w;      // energy balance on stage

                // Reset next stage inlet values.
                stage_P_in = stage_P_out;
                stage_h_in = stage_h_out;

                working_fluid.FindStatueWithPH(stage_P_in, stage_h_in * wmm);
                stage_s_in = working_fluid.Entropy;
            }

            // Note: last stage outlet enthalpy is equivalent to turbomachine outlet enthalpy.

            if (is_comp == true)
            {
                isen_eta = (h_s_out - h_in) / (stage_h_out - h_in);
            }

            else
            {
                isen_eta = (stage_h_out - h_in) / (h_s_out - h_in);
            }
        }

        //Function for calculating Heat Exchanger Conductance (UA) for supercritical Brayton power cycles
        //

        public void calculate_hxr_UA(Int64 N_sub_hxrs, Double Q_dot, Double m_dot_c, Double m_dot_h, Double T_c_in, Double T_h_in, Double P_c_in, Double P_c_out, Double P_h_in, Double P_h_out,
            ref Int64 error_code, ref Double UA, ref Double min_DT, ref Double[] Th1, ref Double[] Tc1, ref Double Effec, ref Double[] Ph1, ref Double[] Pc1, ref Double[] UA_local,
            ref Double NTU_Total, ref Double C_R_Total, ref Double[] NTU, ref Double[] C_R, ref Double[] eff)
        {
            wmm = working_fluid.MolecularWeight;

            // Calculate the conductance (UA value) and minimum temperature difference of a heat exchanger
            // given its mass flow rates, inlet temperatures, and a rate of heat transfer.
            //
            // Inputs:
            //   N_sub_hxrs -- the number of sub-heat exchangers to use for discretization
            //   Q_dot -- rate of heat transfer in the heat exchanger (kW)
            //   m_dot_c -- cold stream mass flow rate (kg/s)
            //   m_dot_h -- hot stream mass flow rate (kg/s)
            //   T_c_in -- cold stream inlet temperature (K)
            //   T_h_in -- hot stream inlet temperature (K)
            //   P_c_in -- cold stream inlet pressure (kPa)
            //   P_c_out -- cold stream outlet pressure (kPa)
            //   P_h_in -- hot stream inlet pressure (kPa)
            //   P_h_out -- hot stream outlet pressure (kPa)
            //
            // Outputs:
            //   error_trace -- an ErrorTrace object
            //   UA -- heat exchanger conductance (kW/K)
            //   min_DT -- minimum temperature difference ("pinch point") between hot and cold streams in heat exchanger (K)
            //
            // Notes:
            //   1) Total pressure drop for each stream is divided equally among the sub-heat exchangers (i.e., DP is a linear distribution).


            //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
            Double TempH, TempC, h_c_in_mol;

            // Local Variables
            Double h_c_in, h_h_in, h_c_out, h_h_out;
            Double[] P_c = new Double[N_sub_hxrs + 1];
            Double[] P_h = new Double[N_sub_hxrs + 1];
            Double[] T_c = new Double[N_sub_hxrs + 1];
            Double[] T_h = new Double[N_sub_hxrs + 1];
            Double[] h_c = new Double[N_sub_hxrs + 1];
            Double[] h_h = new Double[N_sub_hxrs + 1];
            Double[] tempdifferences = new Double[N_sub_hxrs + 1];

            Double[] C_dot_c = new Double[N_sub_hxrs];
            Double[] C_dot_h = new Double[N_sub_hxrs];
            Double[] C_dot_min = new Double[N_sub_hxrs];
            Double[] C_dot_max = new Double[N_sub_hxrs];

            C_R = new Double[N_sub_hxrs];
            eff = new Double[N_sub_hxrs];
            NTU = new Double[N_sub_hxrs];

            // Check inputs.
            if (T_h_in < T_c_in)
            {
                error_code = 5;
                return;
            }

            if (P_h_in < P_h_out)
            {
                error_code = 6;
                return;
            }

            if (P_c_in < P_c_out)
            {
                error_code = 7;
                return;
            }

            if (Math.Abs(Q_dot) <= 1d - 12)  // very low Q_dot; assume it is zero
            {
                UA = 0.0;
                min_DT = T_h_in - T_c_in;
                return;
            }

            // Assume pressure varies linearly through heat exchanger.
            for (int a = 0; a <= N_sub_hxrs; a++)
            {
                P_c[a] = P_c_out + a * (P_c_in - P_c_out) / N_sub_hxrs;
                P_h[a] = P_h_in - a * (P_h_in - P_h_out) / N_sub_hxrs;

                Pc1[a] = P_c[a];
                Ph1[a] = P_h[a];
            }

            // Calculate inlet enthalpies from known state points.

            //if (present(enth)) enth = enth_mol / wmm
            //if (present(entr)) entr = entr_mol / wmm
            //if (present(ssnd)) ssnd = ssnd_RP


            //call CO2_TP(T=T_c_in, P=P_c(N_sub_hxrs+1), error_code=error_code, enth=h_c_in)
            working_fluid.FindStateWithTP(T_c_in, P_c[N_sub_hxrs]);
            h_c_in = working_fluid.Enthalpy;

            //call CO2_TP(T=T_h_in, P=P_h(1), error_code=error_code, enth=h_h_in)
            working_fluid.FindStateWithTP(T_h_in, P_h[0]);
            h_h_in = working_fluid.Enthalpy;

            // Calculate outlet enthalpies from energy balances supporsing 100% Heat transferred
            h_c_out = h_c_in + Q_dot / m_dot_c;
            h_h_out = h_h_in - Q_dot / m_dot_h;

            // Set up the enthalpy vectors and loop through the sub-heat exchangers, calculating temperatures.
            for (int b = 0; b <= N_sub_hxrs; b++)
            {
                h_c[b] = h_c_out + b * (h_c_in - h_c_out) / N_sub_hxrs;  // create linear vector of cold stream enthalpies, with index 1 at the cold stream outlet
                h_h[b] = h_h_in - b * (h_h_in - h_h_out) / N_sub_hxrs;   // create linear vector of hot stream enthalpies, with index 1 at the hot stream inlet
            }

            T_h[0] = T_h_in;  //hot stream inlet temperature

            //IMPORTANT!!!: When calling call CO2_PH is necessary before converting the Enthalpy units from kJ/Kg to J/mol

            wmm = working_fluid.MolecularWeight;


            //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
            TempC = h_c[0] * wmm;
            working_fluid.FindStatueWithPH(P_c[0], TempC);
            T_c[0] = working_fluid.Temperature;

            if (T_c[0] >= T_h[0])  // there was a second law violation in this sub-heat exchanger
            {
                error_code = 11;
                return;
            }

            for (int c = 0; c <= N_sub_hxrs; c++)
            {
                // call CO2_PH(P=P_h(i), H=h_h(i), error_code=error_code, temp=T_h(i))
                //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
                TempH = h_h[c] * wmm;  // convert enthalpy to molar basis
                working_fluid.FindStatueWithPH(P_h[c], TempH);
                T_h[c] = working_fluid.Temperature;

                // call CO2_PH(P=P_c(i), H=h_c(i), error_code=error_code, temp=T_c(i))
                //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
                TempC = h_c[c] * wmm;  // convert enthalpy to molar basis
                working_fluid.FindStatueWithPH(P_c[c], TempC);
                T_c[c] = working_fluid.Temperature;

                if (T_c[c] >= T_h[c])  // there was a second law violation in this sub-heat exchanger
                {
                    error_code = 11;
                    return;
                }
            }

            //UP TO HERE VALIDATED Temperatures and Enthapies

            // Perform effectiveness-NTU and UA calculations (note: the below are all array operations).         
            for (int d = 0; d < N_sub_hxrs; d++)
            {
                C_dot_h[d] = m_dot_h * (h_h[d] - h_h[d + 1]) / (T_h[d] - T_h[d + 1]);  // hot stream capacitance rate
            }

            for (int e = 0; e < N_sub_hxrs; e++)
            {
                C_dot_c[e] = m_dot_c * (h_c[e] - h_c[e + 1]) / (T_c[e] - T_c[e + 1]);  // cold stream capacitance rate
            }

            for (int f = 0; f <= N_sub_hxrs - 1; f++)
            {
                C_dot_min[f] = Math.Min(C_dot_h[f], C_dot_c[f]);  // minimum capacitance stream
                C_dot_max[f] = Math.Max(C_dot_h[f], C_dot_c[f]);  // maximum capacitance stream
                C_R[f] = C_dot_min[f] / C_dot_max[f];
                eff[f] = Q_dot / ((N_sub_hxrs * C_dot_min[f] * (T_h[f] - T_c[f + 1])));  // effectiveness of each sub-heat exchanger

                if (C_R[f] == 1)
                {
                    NTU[f] = eff[f] / (1 - eff[f]);
                }

                else
                {
                    NTU[f] = Math.Log((1 - eff[f] * C_R[f]) / (1 - eff[f])) / (1 - C_R[f]);  // NTU if C_R does not equal 1
                }
            }

            UA = 0;
            NTU_Total = 0;

            for (int g = 0; g <= N_sub_hxrs - 1; g++)
            {
                UA_local[g] = NTU[g] * C_dot_min[g];
                UA = UA + NTU[g] * C_dot_min[g];  // calculate total UA value for the heat exchanger
                NTU_Total = NTU_Total + NTU[g];   // calculate total NTU value for the heat exchanger
                C_R_Total = C_R_Total + C_R[g];   // calculate total C_R_Total value for the heat exchanger
            }

            for (int h = 0; h <= N_sub_hxrs; h++)
            {
                tempdifferences[h] = T_h[h] - T_c[h]; // temperatures differences within the heat exchanger
            }

            min_DT = tempdifferences[0];

            for (int i = 0; i <= N_sub_hxrs; i++)
            {
                if (tempdifferences[i] < min_DT)
                {
                    min_DT = tempdifferences[i]; // find the smallest temperature difference within the heat exchanger
                }

                Th1[i] = T_h[i];
                Tc1[i] = T_c[i];
            }

            // Calculate PHX Effectiveness
            Double C_dot_hot, C_dot_cold, C_dot_min1, Q_dot_max;

            C_dot_hot = m_dot_h * (h_h_in - h_h_out) / (T_h[0] - T_h[15]);   // PHX recuperator hot stream capacitance rate
            C_dot_cold = m_dot_c * (h_c_out - h_c_in) / (T_c[0] - T_c[15]);  // PXH recuperator cold stream capacitance rate
            C_dot_min1 = Math.Min(C_dot_hot, C_dot_cold);
            Q_dot_max = C_dot_min1 * (T_h[0] - T_c[15]);
            Effec = Q_dot / Q_dot_max;  // Definition of effectiveness
        }

        void calculate_hxr_UA_nuevo(Int64 N_hxrs, double Q_dot, double m_dot_c, double m_dot_h, double T_c_in, double T_h_in, double P_c_in, double P_c_out, double P_h_in, double P_h_out,
                                ref int error_code, ref double UA, ref double min_DT)
        {
            /*Calculates the UA of a heat exchanger given its mass flow rates, inlet temperatures, and a heat transfer rate.
            Note: the heat transfer rate must be positive.*/

            // Check inputs
            if (Q_dot < 0.0)
            {
                error_code = 4;
                return;
            }
            if (T_h_in < T_c_in)
            {
                error_code = 5;
                return;
            }
            if (P_h_in < P_h_out)
            {
                error_code = 6;
                return;
            }
            if (P_c_in < P_c_out)
            {
                error_code = 7;
                return;
            }
            if (Q_dot <= 1E-14)      // very low Q_dot; assume it is zero
            {
                UA = 0.0;
                min_DT = T_h_in - T_c_in;
                return;
            }

            // Calculate inlet enthalpies from known state points
            int prop_error_code;

            working_fluid.FindStateWithTP(T_c_in, P_c_in);

            //if (prop_error_code != 0)
            //{
            //    error_code = 8;
            //    return;
            //}
            double h_c_in = working_fluid.Enthalpy;

            working_fluid.FindStateWithTP(T_h_in, P_h_in);
            //prop_error_code = CO2_TP(T_h_in, P_h_in);

            //if (prop_error_code != 0)
            //{
            //    error_code = 9;
            //    return;
            //}
            double h_h_in = working_fluid.Enthalpy;

            // Calculate outlet enthalpies from energy balance
            double h_c_out = h_c_in + Q_dot / m_dot_c;
            double h_h_out = h_h_in - Q_dot / m_dot_h;

            Int64 N_nodes = N_hxrs + 1;
            double h_h_prev = 0.0;
            double T_h_prev = 0.0;
            double h_c_prev = 0.0;
            double T_c_prev = 0.0;
            UA = 0.0;
            min_DT = T_h_in;
            // Loop through the sub-heat exchangers
            for (int i = 0; i < N_nodes; i++)
            {
                // Assume pressure varies linearly through heat exchanger
                double P_c = P_c_out + i * (P_c_in - P_c_out) / (N_nodes - 1);
                double P_h = P_h_in - i * (P_h_in - P_h_out) / (N_nodes - 1);

                // Calculate the entahlpy at the node
                double h_c = h_c_out + i * (h_c_in - h_c_out) / (N_nodes - 1);
                double h_h = h_h_in - i * (h_h_in - h_h_out) / (N_nodes - 1);

                // Calculate the hot and cold temperatures at the node
                //prop_error_code = CO2_PH(P_h, h_h);

                working_fluid.FindStatueWithPH(P_h, h_h * wmm);

                //if (prop_error_code != 0)
                //{
                //    error_code = 12;
                //    return;
                //}
                double T_h = working_fluid.Temperature;

                //prop_error_code = CO2_PH(P_c, h_c);
                working_fluid.FindStatueWithPH(P_c, h_c * wmm);

                //if (prop_error_code != 0)
                //{
                //    error_code = 13;
                //    return;
                //}
                double T_c = working_fluid.Temperature;

                // Check that 2nd law was not violated
                if (T_c >= T_h)
                {
                    error_code = 11;
                    return;
                }

                // Track the minimum temperature difference in the heat exchanger
                min_DT = Math.Min(min_DT, T_h - T_c);

                // Perform effectiveness-NTU and UA calculations 
                if (i > 0)
                {
                    double C_dot_h = m_dot_h * (h_h_prev - h_h) / (T_h_prev - T_h);         // [kW/K] hot stream capacitance rate
                    double C_dot_c = m_dot_c * (h_c_prev - h_c) / (T_c_prev - T_c);         // [kW/K] cold stream capacitance rate
                    double C_dot_min = Math.Min(C_dot_h, C_dot_c);               // [kW/K] Minimum capacitance stream
                    double C_dot_max = Math.Max(C_dot_h, C_dot_c);               // [kW/K] Maximum capacitance stream
                    double C_R = C_dot_min / C_dot_max;                     // [-] Capacitance ratio of sub-heat exchanger
                    double eff = (Q_dot / (double)N_hxrs) / (C_dot_min * (T_h_prev - T_c)); // [-] Effectiveness of each sub-heat exchanger
                    double NTU = 0.0;
                    if (C_R != 1.0)
                        NTU = Math.Log((1.0 - eff * C_R) / (1.0 - eff)) / (1.0 - C_R);       // [-] NTU if C_R does not equal 1
                    else
                        NTU = eff / (1.0 - eff);
                    UA += NTU * C_dot_min;                      // [kW/K] Sum UAs for each hx section			
                }
                h_h_prev = h_h;
                T_h_prev = T_h;
                h_c_prev = h_c;
                T_c_prev = T_c;
            }

            // Check for NaNs that arose
            if (UA != UA)
            {
                error_code = 14;
                return;
            }

            return;
        }

        void calculate_turbomachinery_outlet_nuevo(double T_in, double P_in, double P_out, double eta, bool is_comp, ref int error_code,
            ref double enth_in, ref double entr_in, ref double dens_in, ref double temp_out, ref double enth_out,
            ref double entr_out, ref double dens_out, ref double spec_work)
        {
            /*Calculates the outlet state of a compressor or turbine using its isentropic efficiency.
                is_comp = .true.means the turbomachine is a compressor(w = w_s / eta)
                is_comp = .false.means the turbomachine is a turbine(w = w_s * eta) */

            error_code = 0;

            working_fluid.FindStateWithTP(T_in, P_in);

            double h_in = working_fluid.Enthalpy;
            double s_in = working_fluid.Entropy;
            dens_in = working_fluid.Density;

            working_fluid.FindStatueWithPS(P_out, s_in * wmm);

            double h_s_out = working_fluid.Enthalpy;

            double w_s = h_in - h_s_out;            // specific work if process is isentropic (negative for compression, positive for expansion)

            double w = 0.0;
            if (is_comp)
                w = w_s / eta;                      // actual specific work of compressor (negative)
            else
                w = w_s * eta;                      // actual specific work of turbine (positive)

            double h_out = h_in - w;

            working_fluid.FindStatueWithPH(P_out, h_out * wmm);

            enth_in = h_in;
            entr_in = s_in;
            temp_out = working_fluid.Temperature;
            enth_out = h_out;
            entr_out = working_fluid.Entropy;
            dens_out = working_fluid.Density;
            spec_work = w;

            return;
        }

        //OK reviewed
        public void RecompCycledesign(core luis, ref core.RecompCycle_withoutRH recomp_cycle, Double m_W_dot_net, Double m_T_mc_in,
                             Double m_T_t_in, Double P_mc_in, Double m_P_mc_out, Double DP_LT_c, Double DP_HT_c, Double DP_PC, Double DP_PHX,
                             Double DP_LT_h, Double DP_HT_h, Double UA_LT, Double UA_HT, Double m_recomp_frac, Double m_eta_mc,
                             Double m_eta_rc, Double m_eta_t, Int64 m_N_sub_hxrs, Double m_tol)
        {
            int max_iter = 100;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc = 0.0;
            double w_rc = 0.0;
            double w_t = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;

            int cpp_offset = 1;
            double[] m_temp_last = new double[10];
            double[] m_pres_last = new double[10];
            double[] m_entr_last = new double[10];
            double[] m_enth_last = new double[10];
            double[] m_dens_last = new double[10];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = DP_HT_c;
            m_DP_HT[1] = DP_HT_h;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = DP_LT_c;
            m_DP_LT[1] = DP_LT_h;

            double[] m_DP_PC = new double[2];
            m_DP_PC[1] = DP_PC;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = DP_PHX;

            //double m_eta_mc = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;

            //double m_LT_frac = 0.5;

            //int m_N_sub_hxrs = 15;

            //double m_opt_tol = 0.000001;
            //double m_PR_mc = 3.378;

            //double m_P_mc_out = 25000;

            //double m_recomp_frac = 0.40;

            //double m_tol = 0.00001;
            //double m_T_mc_in = 32 + 273.15;
            //double m_T_t_in = 550 + 273.15;
            //double m_UA_rec_total = 15000;
            //double m_W_dot_net = 50000;

            Int64 error_code;

            double secant_guess;


            //1. CONDICIONES INICIALES

            m_temp_last[1 - cpp_offset] = m_T_mc_in;
            //double P_mc_in = m_P_mc_out / m_PR_mc;
            m_pres_last[1 - cpp_offset] = P_mc_in;
            m_pres_last[2 - cpp_offset] = m_P_mc_out;
            m_temp_last[6 - cpp_offset] = m_T_t_in;



            //2. CÁLCULO DE PRESIONES

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_PC[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC[2 - cpp_offset]));           // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC[2 - cpp_offset];                                        // absolute pressure drop specified for precooler

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];

            int sub_error_code = 0;


            //3. CALCULO DE TURBOMÁQUINAS

            // Determine the outlet states of the main compressor and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc,
                true, ref sub_error_code, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
                ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset],
                ref w_mc);

            if (sub_error_code != 0)
            {
                error_code = 22;
                return;
            }

            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_t,
                false, ref sub_error_code, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset],
                ref w_t);

            if (sub_error_code != 0)
            {
                error_code = 23;
                return;
            }

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                calculate_turbomachinery_outlet_nuevo(m_temp_last[2 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset],
                    m_eta_rc, true, ref sub_error_code, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5],
                    ref dummy[6], ref w_rc);

                if (sub_error_code != 0)
                {
                    error_code = 24;
                    return;
                }
            }

            if (w_mc + w_rc + w_t <= 0.0)
            {
                error_code = 25;
                return;
            }

            //4. BUCLE EXTERIOR DE T8

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;

            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;

            int T8_iter = 0;

            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //call CO2_TP(T=temp(8), P=pres(8), error_code=error_code, enth=enth(8), entr=entr(8), dens=dens(8))
                luis.working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    error_code = 26;
                    return;
                }
                m_enth_last[8 - cpp_offset] = luis.working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = luis.working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = luis.working_fluid.Density;


                //------------------------------------------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------------------------------------------



                //5. BUCLE EXTERIOR DE T9

                // 5.0. CONDICIONES INICIALES DE T9

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;

                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
               

                int T9_iter = 0;
                
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // 5.1. CALCULO DEL RECOMPRESOR: Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        calculate_turbomachinery_outlet_nuevo(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code, ref m_enth_last[9 - cpp_offset], ref m_entr_last[9 - cpp_offset], ref m_dens_last[9 - cpp_offset],
                            ref m_temp_last[10 - cpp_offset], ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset],
                            ref w_rc);

                        if (sub_error_code != 0)
                        {
                            error_code = 27;
                            return;
                        }
                    }

                    //5.1 LA FRACCIÓN DEL RECOMPRESIÓN ES CERO. No hay que calcular las condicones a la salida del recompresor.
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(9), P=pres(9), error_code=error_code, enth=enth(9), entr=entr(9), dens=dens(9));  // fully define state 9
                        luis.working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[9 - cpp_offset];                 // Assume state(10) is the same as state(9)
                        m_enth_last[9 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[9 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[9 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // 5.2 Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / (w_mc * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_t);            // total mass flow rate(through turbine)
                    
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        error_code = 29;
                        return;
                    }
                    
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    
                    m_dot_mc = m_dot_t - m_dot_rc;

                    // 5.3. Calculate the UA value of the low-temperature recuperator (LTR).
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code, ref UA_LT_calc, ref min_DT_LT);
                    
                    // 5.4. Comprobar que ha dado error el cálculo del LTR 
                    if (sub_error_code > 0)
                    {
                        if (sub_error_code == 11)       // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            error_code = 30;
                            return;
                        }
                    }

                    // 5.5. Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                   
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // 5.6 FINAL DEL BUCLE DE T9. End iteration T9

                // 5.7. Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    error_code = 31;
                    return;
                }


                //------------------------------------------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------------------------------------------



                // 6. CÁCULO DEL PUNTO 3. State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset], &co2_props);

                wmm = luis.working_fluid.MolecularWeight;

                luis.working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);
                
                //call CO2_PH(P=pres(3), H=enth(3), error_code=error_code, temp=temp(3), entr=entr(3), dens=dens(3))

                if (property_error_code != 0)
                {
                    error_code = 32;
                    return;
                }

                m_temp_last[3 - cpp_offset] = luis.working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = luis.working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = luis.working_fluid.Density;




                // 7. Go through MIXING VALVE
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset], &co2_props);
                    wmm = luis.working_fluid.MolecularWeight;
                    luis.working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        error_code = 33;
                        return;
                    }

                    m_temp_last[4 - cpp_offset] = luis.working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = luis.working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = luis.working_fluid.Density;
                }

                // NO MIXING VALVE, therefore (4) is equal to (3)
                else
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code > 0)
                {
                    if (sub_error_code == 1)        // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        error_code = 34;
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess2 = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess2 <= T8_lower_bound || secant_guess2 >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess2;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                error_code = 35;
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset], &co2_props);
            wmm = luis.working_fluid.MolecularWeight;
            luis.working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            if (property_error_code != 0)
            {
                error_code = 36;
                return;
            }

            m_temp_last[5 - cpp_offset] = luis.working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = luis.working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = luis.working_fluid.Density;

            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);

            // Recompression Cycle
            double m_W_dot_net_last = w_mc * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t;
            double m_eta_thermal_last = m_W_dot_net_last / Q_dot_PHX;

            // Set cycle state point properties.
            recomp_cycle.temp = m_temp_last;
            recomp_cycle.pres = m_pres_last;
            recomp_cycle.enth = m_enth_last;
            recomp_cycle.entr = m_entr_last;
            recomp_cycle.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            recomp_cycle.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            recomp_cycle.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(recomp_cycle.LT.C_dot_hot, recomp_cycle.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            recomp_cycle.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            recomp_cycle.LT.UA_design = UA_LT_calc;
            recomp_cycle.LT.UA = UA_LT_calc;
            recomp_cycle.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            recomp_cycle.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            recomp_cycle.LT.m_dot_design[0] = m_dot_mc;
            recomp_cycle.LT.m_dot_design[1] = m_dot_t;
            recomp_cycle.LT.T_c_in = m_temp_last[2 - cpp_offset];
            recomp_cycle.LT.T_h_in = m_temp_last[8 - cpp_offset];
            recomp_cycle.LT.P_c_in = m_pres_last[2 - cpp_offset];
            recomp_cycle.LT.P_h_in = m_pres_last[8 - cpp_offset];
            recomp_cycle.LT.P_c_out = m_pres_last[3 - cpp_offset];
            recomp_cycle.LT.P_h_out = m_pres_last[9 - cpp_offset];
            recomp_cycle.LT.Q_dot = Q_dot_LT;
            recomp_cycle.LT.min_DT = min_DT_LT;
            recomp_cycle.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            recomp_cycle.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            recomp_cycle.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(recomp_cycle.HT.C_dot_hot, recomp_cycle.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            recomp_cycle.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            recomp_cycle.HT.UA_design = UA_HT_calc;
            recomp_cycle.HT.UA = UA_HT_calc;
            recomp_cycle.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            recomp_cycle.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            recomp_cycle.HT.m_dot_design[0] = m_dot_t;
            recomp_cycle.HT.m_dot_design[1] = m_dot_t;
            recomp_cycle.HT.T_c_in = m_temp_last[4 - cpp_offset];
            recomp_cycle.HT.T_h_in = m_temp_last[7 - cpp_offset];
            recomp_cycle.HT.P_c_in = m_pres_last[4 - cpp_offset];
            recomp_cycle.HT.P_h_in = m_pres_last[7 - cpp_offset];
            recomp_cycle.HT.P_c_out = m_pres_last[5 - cpp_offset];
            recomp_cycle.HT.P_h_out = m_pres_last[8 - cpp_offset];
            recomp_cycle.HT.Q_dot = Q_dot_HT;
            recomp_cycle.HT.min_DT = min_DT_HT;
            recomp_cycle.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            recomp_cycle.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            recomp_cycle.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            recomp_cycle.PHX.DP_design2 = 0.0;
            //recomp_cycle%PHX%m_dot_design = [m_dot_t, 0.0_dp]

            recomp_cycle.PC.Q_dot = m_dot_mc * (m_enth_last[9 - cpp_offset] - m_enth_last[1 - cpp_offset]);
            recomp_cycle.PC.DP_design1 = 0.0;
            recomp_cycle.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[1 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            // Calculate cycle performance metrics.
            recomp_cycle.recomp_frac = m_recomp_frac;

            recomp_cycle.W_dot_net = w_mc * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t;

            recomp_cycle.eta_thermal = recomp_cycle.W_dot_net / (recomp_cycle.PHX.Q_dot);

            recomp_cycle.m_dot_turbine = m_dot_t;
            recomp_cycle.conv_tol = m_tol;

            return;
        }
        
        //OK reviewed
        public void RecompCycledesign_for_Optimization(core luis, ref core.RecompCycle_withoutRH recomp_cycle, Double m_W_dot_net, Double m_T_mc_in,
                            Double m_T_t_in, Double P_mc_in, Double m_P_mc_out, Double DP_LT_c, Double DP_HT_c, Double DP_PC, Double DP_PHX,
                            Double DP_LT_h, Double DP_HT_h, Double LT_fraction, Double UA_Total, Double m_recomp_frac, Double m_eta_mc,
                            Double m_eta_rc, Double m_eta_t, Int64 m_N_sub_hxrs, Double m_tol)
        {
            double UA_LT = UA_Total * LT_fraction;
            double UA_HT = UA_Total * (1 - LT_fraction);

            int max_iter = 100;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc = 0.0;
            double w_rc = 0.0;
            double w_t = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;          

            int cpp_offset = 1;
            double[] m_temp_last = new double[10];
            double[] m_pres_last = new double[10];
            double[] m_entr_last = new double[10];
            double[] m_enth_last = new double[10];
            double[] m_dens_last = new double[10];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = DP_HT_c;
            m_DP_HT[1] = DP_HT_h;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = DP_LT_c;
            m_DP_LT[1] = DP_LT_h;

            double[] m_DP_PC = new double[2];
            m_DP_PC[1] = DP_PC;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = DP_PHX;

            //double m_eta_mc = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;

            //double m_LT_frac = 0.5;

            //int m_N_sub_hxrs = 15;

            //double m_opt_tol = 0.000001;
            //double m_PR_mc = 3.378;

            //double m_P_mc_out = 25000;

            //double m_recomp_frac = 0.40;

            //double m_tol = 0.00001;
            //double m_T_mc_in = 32 + 273.15;
            //double m_T_t_in = 550 + 273.15;
            //double m_UA_rec_total = 15000;
            //double m_W_dot_net = 50000;

            Int64 error_code;

            double secant_guess;

            m_temp_last[1 - cpp_offset] = m_T_mc_in;
            //double P_mc_in = m_P_mc_out / m_PR_mc;
            m_pres_last[1 - cpp_offset] = P_mc_in;
            m_pres_last[2 - cpp_offset] = m_P_mc_out;
            m_temp_last[6 - cpp_offset] = m_T_t_in;

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_PC[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC[2 - cpp_offset]));           // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC[2 - cpp_offset];                                        // absolute pressure drop specified for precooler

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];

            int sub_error_code = 0;

            // Determine the outlet states of the main compressor and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc,
                true, ref sub_error_code, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
                ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset],
                ref w_mc);

            if (sub_error_code != 0)
            {
                error_code = 22;
                return;
            }

            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_t,
                false, ref sub_error_code, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset],
                ref w_t);

            if (sub_error_code != 0)
            {
                error_code = 23;
                return;
            }

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                calculate_turbomachinery_outlet_nuevo(m_temp_last[2 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset],
                    m_eta_rc, true, ref sub_error_code, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5],
                    ref dummy[6], ref w_rc);

                if (sub_error_code != 0)
                {
                    error_code = 24;
                    return;
                }
            }

            if (w_mc + w_rc + w_t <= 0.0)
            {
                error_code = 25;
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;
            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //call CO2_TP(T=temp(8), P=pres(8), error_code=error_code, enth=enth(8), entr=entr(8), dens=dens(8))
                luis.working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    error_code = 26;
                    return;
                }
                m_enth_last[8 - cpp_offset] = luis.working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = luis.working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = luis.working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;

                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        calculate_turbomachinery_outlet_nuevo(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code, ref m_enth_last[9 - cpp_offset], ref m_entr_last[9 - cpp_offset], ref m_dens_last[9 - cpp_offset],
                            ref m_temp_last[10 - cpp_offset], ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset],
                            ref w_rc);

                        if (sub_error_code != 0)
                        {
                            error_code = 27;
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(9), P=pres(9), error_code=error_code, enth=enth(9), entr=entr(9), dens=dens(9));  // fully define state 9
                        luis.working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[9 - cpp_offset];                 // Assume state(10) is the same as state(9)
                        m_enth_last[9 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[9 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[9 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / (w_mc * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_t);            // total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        error_code = 29;
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code > 0)
                    {
                        if (sub_error_code == 11)       // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            error_code = 30;
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    error_code = 31;
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset], &co2_props);

                wmm = luis.working_fluid.MolecularWeight;
                luis.working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);
                //call CO2_PH(P=pres(3), H=enth(3), error_code=error_code, temp=temp(3), entr=entr(3), dens=dens(3))

                if (property_error_code != 0)
                {
                    error_code = 32;
                    return;
                }

                m_temp_last[3 - cpp_offset] = luis.working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = luis.working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = luis.working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset], &co2_props);
                    wmm = luis.working_fluid.MolecularWeight;
                    luis.working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        error_code = 33;
                        return;
                    }

                    m_temp_last[4 - cpp_offset] = luis.working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = luis.working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = luis.working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code > 0)
                {
                    if (sub_error_code == 1)        // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        error_code = 34;
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess2 = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess2 <= T8_lower_bound || secant_guess2 >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess2;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                error_code = 35;
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset], &co2_props);
            wmm = luis.working_fluid.MolecularWeight;
            luis.working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            if (property_error_code != 0)
            {
                error_code = 36;
                return;
            }

            m_temp_last[5 - cpp_offset] = luis.working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = luis.working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = luis.working_fluid.Density;

            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);

            // Recompression Cycle
            double m_W_dot_net_last = w_mc * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t;
            double m_eta_thermal_last = m_W_dot_net_last / Q_dot_PHX;

            // Set cycle state point properties.
            recomp_cycle.temp = m_temp_last;
            recomp_cycle.pres = m_pres_last;
            recomp_cycle.enth = m_enth_last;
            recomp_cycle.entr = m_entr_last;
            recomp_cycle.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            recomp_cycle.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            recomp_cycle.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(recomp_cycle.LT.C_dot_hot, recomp_cycle.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            recomp_cycle.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            recomp_cycle.LT.UA_design = UA_LT_calc;
            recomp_cycle.LT.UA = UA_LT_calc;
            recomp_cycle.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            recomp_cycle.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            recomp_cycle.LT.m_dot_design[0] = m_dot_mc;
            recomp_cycle.LT.m_dot_design[1] = m_dot_t;
            recomp_cycle.LT.T_c_in = m_temp_last[2 - cpp_offset];
            recomp_cycle.LT.T_h_in = m_temp_last[8 - cpp_offset];
            recomp_cycle.LT.P_c_in = m_pres_last[2 - cpp_offset];
            recomp_cycle.LT.P_h_in = m_pres_last[8 - cpp_offset];
            recomp_cycle.LT.P_c_out = m_pres_last[3 - cpp_offset];
            recomp_cycle.LT.P_h_out = m_pres_last[9 - cpp_offset];
            recomp_cycle.LT.Q_dot = Q_dot_LT;
            recomp_cycle.LT.min_DT = min_DT_LT;
            recomp_cycle.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            recomp_cycle.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            recomp_cycle.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(recomp_cycle.HT.C_dot_hot, recomp_cycle.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            recomp_cycle.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            recomp_cycle.HT.UA_design = UA_HT_calc;
            recomp_cycle.HT.UA = UA_HT_calc;
            recomp_cycle.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            recomp_cycle.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            recomp_cycle.HT.m_dot_design[0] = m_dot_t;
            recomp_cycle.HT.m_dot_design[1] = m_dot_t;
            recomp_cycle.HT.T_c_in = m_temp_last[4 - cpp_offset];
            recomp_cycle.HT.T_h_in = m_temp_last[7 - cpp_offset];
            recomp_cycle.HT.P_c_in = m_pres_last[4 - cpp_offset];
            recomp_cycle.HT.P_h_in = m_pres_last[7 - cpp_offset];
            recomp_cycle.HT.P_c_out = m_pres_last[5 - cpp_offset];
            recomp_cycle.HT.P_h_out = m_pres_last[8 - cpp_offset];
            recomp_cycle.HT.Q_dot = Q_dot_HT;
            recomp_cycle.HT.min_DT = min_DT_HT;
            recomp_cycle.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            recomp_cycle.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            recomp_cycle.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            recomp_cycle.PHX.DP_design2 = 0.0;
            //recomp_cycle%PHX%m_dot_design = [m_dot_t, 0.0_dp]

            recomp_cycle.PC.Q_dot = m_dot_mc * (m_enth_last[9 - cpp_offset] - m_enth_last[1 - cpp_offset]);
            recomp_cycle.PC.DP_design1 = 0.0;
            recomp_cycle.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[1 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            // Calculate cycle performance metrics.
            recomp_cycle.recomp_frac = m_recomp_frac;

            recomp_cycle.W_dot_net = w_mc * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t;

            recomp_cycle.eta_thermal = recomp_cycle.W_dot_net / (recomp_cycle.PHX.Q_dot);

            recomp_cycle.m_dot_turbine = m_dot_t;
            recomp_cycle.conv_tol = m_tol;

            return;
        }

        //OK reviewed
        public void RecompCycledesign_withReheating(core luis, ref core.RecompCycle recomp_cycle, Double m_W_dot_net, Double m_T_mc_in,
                            Double m_T_mt_in, Double P_mc_in, Double m_P_mc_out, Double m_P_rt_in, Double m_T_rt_in, Double DP_LT_c, Double DP_HT_c,
                            Double DP_PC, Double DP_PHX, Double DP_RHX, Double DP_LT_h, Double DP_HT_h, Double UA_LT, Double UA_HT, Double m_recomp_frac,
                            Double m_eta_mc, Double m_eta_rc, Double m_eta_t, Double m_eta_rt, Int64 m_N_sub_hxrs, Double m_tol)
        {
            int max_iter = 500;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc = 0.0;
            double w_rc = 0.0;
            double w_mt = 0.0;
            double w_rt = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;

            int cpp_offset = 1;
            double[] m_temp_last = new double[12];
            double[] m_pres_last = new double[12];
            double[] m_entr_last = new double[12];
            double[] m_enth_last = new double[12];
            double[] m_dens_last = new double[12];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = DP_HT_c;
            m_DP_HT[1] = DP_HT_h;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = DP_LT_c;
            m_DP_LT[1] = DP_LT_h;

            double[] m_DP_PC = new double[2];
            m_DP_PC[1] = DP_PC;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = DP_PHX;

            double[] m_DP_RHX = new double[2];
            m_DP_RHX[0] = DP_RHX;

            //double m_eta_mc = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;

            //double m_LT_frac = 0.5;

            //int m_N_sub_hxrs = 15;

            //double m_opt_tol = 0.000001;
            //double m_PR_mc = 3.378;

            //double m_P_mc_out = 25000;

            //double m_recomp_frac = 0.40;

            //double m_tol = 0.00001;
            //double m_T_mc_in = 32 + 273.15;
            //double m_T_t_in = 550 + 273.15;
            //double m_UA_rec_total = 15000;
            //double m_W_dot_net = 50000;

            Int64 error_code;

            double secant_guess;

            m_temp_last[1 - cpp_offset] = m_T_mc_in;
            //double P_mc_in = m_P_mc_out / m_PR_mc;
            m_pres_last[1 - cpp_offset] = P_mc_in;
            m_pres_last[2 - cpp_offset] = m_P_mc_out;
            m_temp_last[6 - cpp_offset] = m_T_mt_in;
            m_temp_last[12 - cpp_offset] = m_T_rt_in;
            m_pres_last[12 - cpp_offset] = m_P_rt_in;

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_RHX[1 - cpp_offset] < 0.0)
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] / (1.0 - Math.Abs(m_DP_RHX[1 - cpp_offset])); // relative pressure drop specified for PHX
            else
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] + m_DP_RHX[1 - cpp_offset];                             // absolute pressure drop specified for PHX

            if (m_DP_PC[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC[2 - cpp_offset]));           // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC[2 - cpp_offset];                                        // absolute pressure drop specified for precooler

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];


            int sub_error_code = 0;

            // Determine the outlet states of the main compressor and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc,
         true, ref sub_error_code, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
         ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset], ref w_mc);

            if (sub_error_code != 0)
            {
                error_code = 22;
                return;
            }

            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[11 - cpp_offset], m_eta_t,
                false, ref sub_error_code, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[11 - cpp_offset], ref m_enth_last[11 - cpp_offset], ref m_entr_last[11 - cpp_offset], ref m_dens_last[11 - cpp_offset],
                ref w_mt);

            //Reheating Turbine
            calculate_turbomachinery_outlet_nuevo(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_rt,
                false, ref sub_error_code, ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset], ref w_rt);


            if (sub_error_code != 0)
            {
                error_code = 23;
                return;
            }

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                calculate_turbomachinery_outlet_nuevo(m_temp_last[2 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset],
                    m_eta_rc, true, ref sub_error_code, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5],
                    ref dummy[6], ref w_rc);

                if (sub_error_code != 0)
                {
                    error_code = 24;
                    return;
                }
            }

            if (w_mc + w_rc + w_mt + w_rt <= 0.0)
            {
                error_code = 25;
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;
            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //call CO2_TP(T=temp(8), P=pres(8), error_code=error_code, enth=enth(8), entr=entr(8), dens=dens(8))
                luis.working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    error_code = 26;
                    return;
                }
                m_enth_last[8 - cpp_offset] = luis.working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = luis.working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = luis.working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;

                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        calculate_turbomachinery_outlet_nuevo(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code, ref m_enth_last[9 - cpp_offset], ref m_entr_last[9 - cpp_offset], ref m_dens_last[9 - cpp_offset],
                            ref m_temp_last[10 - cpp_offset], ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset],
                            ref w_rc);

                        if (sub_error_code != 0)
                        {
                            error_code = 27;
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(9), P=pres(9), error_code=error_code, enth=enth(9), entr=entr(9), dens=dens(9));  // fully define state 9
                        luis.working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[9 - cpp_offset];                 // Assume state(10) is the same as state(9)
                        m_enth_last[9 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[9 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[9 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / (w_mc * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_mt + w_rt);			// total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        error_code = 29;
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code > 0)
                    {
                        if (sub_error_code == 11)       // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            error_code = 30;
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    error_code = 31;
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset], &co2_props);

                wmm = luis.working_fluid.MolecularWeight;
                luis.working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);
                //call CO2_PH(P=pres(3), H=enth(3), error_code=error_code, temp=temp(3), entr=entr(3), dens=dens(3))

                if (property_error_code != 0)
                {
                    error_code = 32;
                    return;
                }

                m_temp_last[3 - cpp_offset] = luis.working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = luis.working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = luis.working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset], &co2_props);
                    wmm = luis.working_fluid.MolecularWeight;
                    luis.working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        error_code = 33;
                        return;
                    }

                    m_temp_last[4 - cpp_offset] = luis.working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = luis.working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = luis.working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code > 0)
                {
                    if (sub_error_code == 1)        // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        error_code = 34;
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess2 = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess2 <= T8_lower_bound || secant_guess2 >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess2;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                error_code = 35;
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset], &co2_props);
            wmm = luis.working_fluid.MolecularWeight;
            luis.working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            if (property_error_code != 0)
            {
                error_code = 36;
                return;
            }

            m_temp_last[5 - cpp_offset] = luis.working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = luis.working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = luis.working_fluid.Density;

            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            double Q_dot_RHX = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);

            // Recompression Cycle
            double m_W_dot_net_last = w_mc * m_dot_mc + w_rc * m_dot_rc + w_mt * m_dot_t + w_rt * m_dot_t;
            double m_eta_thermal_last = m_W_dot_net_last / (Q_dot_PHX + Q_dot_RHX);

            // Set cycle state point properties.
            recomp_cycle.temp = m_temp_last;
            recomp_cycle.pres = m_pres_last;
            recomp_cycle.enth = m_enth_last;
            recomp_cycle.entr = m_entr_last;
            recomp_cycle.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            recomp_cycle.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            recomp_cycle.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(recomp_cycle.LT.C_dot_hot, recomp_cycle.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            recomp_cycle.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            recomp_cycle.LT.UA_design = UA_LT_calc;
            recomp_cycle.LT.UA = UA_LT_calc;
            recomp_cycle.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            recomp_cycle.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            recomp_cycle.LT.m_dot_design[0] = m_dot_mc;
            recomp_cycle.LT.m_dot_design[1] = m_dot_t;
            recomp_cycle.LT.T_c_in = m_temp_last[2 - cpp_offset];
            recomp_cycle.LT.T_h_in = m_temp_last[8 - cpp_offset];
            recomp_cycle.LT.P_c_in = m_pres_last[2 - cpp_offset];
            recomp_cycle.LT.P_h_in = m_pres_last[8 - cpp_offset];
            recomp_cycle.LT.P_c_out = m_pres_last[3 - cpp_offset];
            recomp_cycle.LT.P_h_out = m_pres_last[9 - cpp_offset];
            recomp_cycle.LT.Q_dot = Q_dot_LT;
            recomp_cycle.LT.min_DT = min_DT_LT;
            recomp_cycle.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            recomp_cycle.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            recomp_cycle.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(recomp_cycle.HT.C_dot_hot, recomp_cycle.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            recomp_cycle.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            recomp_cycle.HT.UA_design = UA_HT_calc;
            recomp_cycle.HT.UA = UA_HT_calc;
            recomp_cycle.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            recomp_cycle.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            recomp_cycle.HT.m_dot_design[0] = m_dot_t;
            recomp_cycle.HT.m_dot_design[1] = m_dot_t;
            recomp_cycle.HT.T_c_in = m_temp_last[4 - cpp_offset];
            recomp_cycle.HT.T_h_in = m_temp_last[7 - cpp_offset];
            recomp_cycle.HT.P_c_in = m_pres_last[4 - cpp_offset];
            recomp_cycle.HT.P_h_in = m_pres_last[7 - cpp_offset];
            recomp_cycle.HT.P_c_out = m_pres_last[5 - cpp_offset];
            recomp_cycle.HT.P_h_out = m_pres_last[8 - cpp_offset];
            recomp_cycle.HT.Q_dot = Q_dot_HT;
            recomp_cycle.HT.min_DT = min_DT_HT;
            recomp_cycle.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            recomp_cycle.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            recomp_cycle.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            recomp_cycle.PHX.DP_design2 = 0.0;
            //recomp_cycle%PHX%m_dot_design = [m_dot_t, 0.0_dp]

            recomp_cycle.RHX.Q_dot = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);
            recomp_cycle.RHX.DP_design1 = m_pres_last[11 - cpp_offset] - m_pres_last[12 - cpp_offset];
            recomp_cycle.RHX.DP_design2 = 0.0;

            recomp_cycle.PC.Q_dot = m_dot_mc * (m_enth_last[9 - cpp_offset] - m_enth_last[1 - cpp_offset]);
            recomp_cycle.PC.DP_design1 = 0.0;
            recomp_cycle.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[1 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            // Calculate cycle performance metrics.
            recomp_cycle.recomp_frac = m_recomp_frac;

            recomp_cycle.W_dot_net = w_mc * m_dot_mc + w_rc * m_dot_rc + w_mt * m_dot_t + w_rt * m_dot_t;

            recomp_cycle.eta_thermal = recomp_cycle.W_dot_net / (recomp_cycle.PHX.Q_dot + recomp_cycle.RHX.Q_dot);

            recomp_cycle.m_dot_turbine = m_dot_t;
            recomp_cycle.conv_tol = m_tol;

            return;
        }

        //OK reviewed
        public void RecompCycledesign_withReheating_for_Optimization(core luis, ref core.RecompCycle recomp_cycle, Double m_W_dot_net, Double m_T_mc_in,
                           Double m_T_mt_in, Double P_mc_in, Double m_P_mc_out, Double m_P_rt_in, Double m_T_rt_in, Double DP_LT_c, Double DP_HT_c,
                           Double DP_PC, Double DP_PHX, Double DP_RHX, Double DP_LT_h, Double DP_HT_h, Double LT_fraction, Double UA_Total, Double m_recomp_frac,
                           Double m_eta_mc, Double m_eta_rc, Double m_eta_t, Double m_eta_rt, Int64 m_N_sub_hxrs, Double m_tol)
        {
            double UA_LT = UA_Total * LT_fraction;
            double UA_HT = UA_Total * (1 - LT_fraction);

            int max_iter = 500;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc = 0.0;
            double w_rc = 0.0;
            double w_mt = 0.0;
            double w_rt = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;

            int cpp_offset = 1;
            double[] m_temp_last = new double[12];
            double[] m_pres_last = new double[12];
            double[] m_entr_last = new double[12];
            double[] m_enth_last = new double[12];
            double[] m_dens_last = new double[12];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = DP_HT_c;
            m_DP_HT[1] = DP_HT_h;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = DP_LT_c;
            m_DP_LT[1] = DP_LT_h;

            double[] m_DP_PC = new double[2];
            m_DP_PC[1] = DP_PC;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = DP_PHX;

            double[] m_DP_RHX = new double[2];
            m_DP_RHX[0] = DP_RHX;

            //double m_eta_mc = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;

            //double m_LT_frac = 0.5;

            //int m_N_sub_hxrs = 15;

            //double m_opt_tol = 0.000001;
            //double m_PR_mc = 3.378;

            //double m_P_mc_out = 25000;

            //double m_recomp_frac = 0.40;

            //double m_tol = 0.00001;
            //double m_T_mc_in = 32 + 273.15;
            //double m_T_t_in = 550 + 273.15;
            //double m_UA_rec_total = 15000;
            //double m_W_dot_net = 50000;

            Int64 error_code;

            double secant_guess;

            m_temp_last[1 - cpp_offset] = m_T_mc_in;
            //double P_mc_in = m_P_mc_out / m_PR_mc;
            m_pres_last[1 - cpp_offset] = P_mc_in;
            m_pres_last[2 - cpp_offset] = m_P_mc_out;
            m_temp_last[6 - cpp_offset] = m_T_mt_in;
            m_temp_last[12 - cpp_offset] = m_T_rt_in;
            m_pres_last[12 - cpp_offset] = m_P_rt_in;

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_RHX[1 - cpp_offset] < 0.0)
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] + m_pres_last[12 - cpp_offset] * Math.Abs(m_DP_RHX[1 - cpp_offset]); // relative pressure drop specified for PHX
            else
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] + m_DP_RHX[1 - cpp_offset];                             // absolute pressure drop specified for PHX

            if (m_DP_PC[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC[2 - cpp_offset]));           // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC[2 - cpp_offset];                                        // absolute pressure drop specified for precooler

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];


            int sub_error_code = 0;

            // Determine the outlet states of the main compressor and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc,
         true, ref sub_error_code, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
         ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset], ref w_mc);

            if (sub_error_code != 0)
            {
                error_code = 22;
                return;
            }

            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[11 - cpp_offset], m_eta_t,
                false, ref sub_error_code, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[11 - cpp_offset], ref m_enth_last[11 - cpp_offset], ref m_entr_last[11 - cpp_offset], ref m_dens_last[11 - cpp_offset],
                ref w_mt);

            //Reheating Turbine
            calculate_turbomachinery_outlet_nuevo(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_rt,
                false, ref sub_error_code, ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset], ref w_rt);


            if (sub_error_code != 0)
            {
                error_code = 23;
                return;
            }

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                calculate_turbomachinery_outlet_nuevo(m_temp_last[2 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset],
                    m_eta_rc, true, ref sub_error_code, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5],
                    ref dummy[6], ref w_rc);

                if (sub_error_code != 0)
                {
                    error_code = 24;
                    return;
                }
            }

            if (w_mc + w_rc + w_mt + w_rt <= 0.0)
            {
                error_code = 25;
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;
            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //call CO2_TP(T=temp(8), P=pres(8), error_code=error_code, enth=enth(8), entr=entr(8), dens=dens(8))
                luis.working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    error_code = 26;
                    return;
                }
                m_enth_last[8 - cpp_offset] = luis.working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = luis.working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = luis.working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;

                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        calculate_turbomachinery_outlet_nuevo(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code, ref m_enth_last[9 - cpp_offset], ref m_entr_last[9 - cpp_offset], ref m_dens_last[9 - cpp_offset],
                            ref m_temp_last[10 - cpp_offset], ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset],
                            ref w_rc);

                        if (sub_error_code != 0)
                        {
                            error_code = 27;
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(9), P=pres(9), error_code=error_code, enth=enth(9), entr=entr(9), dens=dens(9));  // fully define state 9
                        luis.working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[9 - cpp_offset];                 // Assume state(10) is the same as state(9)
                        m_enth_last[9 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[9 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[9 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / (w_mc * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_mt + w_rt);			// total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        error_code = 29;
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code > 0)
                    {
                        if (sub_error_code == 11)       // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            error_code = 30;
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    error_code = 31;
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset], &co2_props);

                wmm = luis.working_fluid.MolecularWeight;
                luis.working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);
                //call CO2_PH(P=pres(3), H=enth(3), error_code=error_code, temp=temp(3), entr=entr(3), dens=dens(3))

                if (property_error_code != 0)
                {
                    error_code = 32;
                    return;
                }

                m_temp_last[3 - cpp_offset] = luis.working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = luis.working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = luis.working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset], &co2_props);
                    wmm = luis.working_fluid.MolecularWeight;
                    luis.working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        error_code = 33;
                        return;
                    }

                    m_temp_last[4 - cpp_offset] = luis.working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = luis.working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = luis.working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code > 0)
                {
                    if (sub_error_code == 1)        // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        error_code = 34;
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess2 = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess2 <= T8_lower_bound || secant_guess2 >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess2;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                error_code = 35;
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset], &co2_props);
            wmm = luis.working_fluid.MolecularWeight;
            luis.working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            if (property_error_code != 0)
            {
                error_code = 36;
                return;
            }

            m_temp_last[5 - cpp_offset] = luis.working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = luis.working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = luis.working_fluid.Density;

            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            double Q_dot_RHX = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);

            // Recompression Cycle
            double m_W_dot_net_last = w_mc * m_dot_mc + w_rc * m_dot_rc + w_mt * m_dot_t + w_rt * m_dot_t;
            double m_eta_thermal_last = m_W_dot_net_last / (Q_dot_PHX + Q_dot_RHX);

            // Set cycle state point properties.
            recomp_cycle.temp = m_temp_last;
            recomp_cycle.pres = m_pres_last;
            recomp_cycle.enth = m_enth_last;
            recomp_cycle.entr = m_entr_last;
            recomp_cycle.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            recomp_cycle.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            recomp_cycle.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(recomp_cycle.LT.C_dot_hot, recomp_cycle.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            recomp_cycle.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            recomp_cycle.LT.UA_design = UA_LT_calc;
            recomp_cycle.LT.UA = UA_LT_calc;
            recomp_cycle.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            recomp_cycle.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            recomp_cycle.LT.m_dot_design[0] = m_dot_mc;
            recomp_cycle.LT.m_dot_design[1] = m_dot_t;
            recomp_cycle.LT.T_c_in = m_temp_last[2 - cpp_offset];
            recomp_cycle.LT.T_h_in = m_temp_last[8 - cpp_offset];
            recomp_cycle.LT.P_c_in = m_pres_last[2 - cpp_offset];
            recomp_cycle.LT.P_h_in = m_pres_last[8 - cpp_offset];
            recomp_cycle.LT.P_c_out = m_pres_last[3 - cpp_offset];
            recomp_cycle.LT.P_h_out = m_pres_last[9 - cpp_offset];
            recomp_cycle.LT.Q_dot = Q_dot_LT;
            recomp_cycle.LT.min_DT = min_DT_LT;
            recomp_cycle.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            recomp_cycle.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            recomp_cycle.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(recomp_cycle.HT.C_dot_hot, recomp_cycle.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            recomp_cycle.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            recomp_cycle.HT.UA_design = UA_HT_calc;
            recomp_cycle.HT.UA = UA_HT_calc;
            recomp_cycle.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            recomp_cycle.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            recomp_cycle.HT.m_dot_design[0] = m_dot_t;
            recomp_cycle.HT.m_dot_design[1] = m_dot_t;
            recomp_cycle.HT.T_c_in = m_temp_last[4 - cpp_offset];
            recomp_cycle.HT.T_h_in = m_temp_last[7 - cpp_offset];
            recomp_cycle.HT.P_c_in = m_pres_last[4 - cpp_offset];
            recomp_cycle.HT.P_h_in = m_pres_last[7 - cpp_offset];
            recomp_cycle.HT.P_c_out = m_pres_last[5 - cpp_offset];
            recomp_cycle.HT.P_h_out = m_pres_last[8 - cpp_offset];
            recomp_cycle.HT.Q_dot = Q_dot_HT;
            recomp_cycle.HT.min_DT = min_DT_HT;
            recomp_cycle.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            recomp_cycle.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            recomp_cycle.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            recomp_cycle.PHX.DP_design2 = 0.0;
            //recomp_cycle%PHX%m_dot_design = [m_dot_t, 0.0_dp]

            recomp_cycle.RHX.Q_dot = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);
            recomp_cycle.RHX.DP_design1 = m_pres_last[11 - cpp_offset] - m_pres_last[12 - cpp_offset];
            recomp_cycle.RHX.DP_design2 = 0.0;

            recomp_cycle.PC.Q_dot = m_dot_mc * (m_enth_last[9 - cpp_offset] - m_enth_last[1 - cpp_offset]);
            recomp_cycle.PC.DP_design1 = 0.0;
            recomp_cycle.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[1 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            // Calculate cycle performance metrics.
            recomp_cycle.recomp_frac = m_recomp_frac;

            recomp_cycle.W_dot_net = w_mc * m_dot_mc + w_rc * m_dot_rc + w_mt * m_dot_t + w_rt * m_dot_t;

            recomp_cycle.eta_thermal = recomp_cycle.W_dot_net / (recomp_cycle.PHX.Q_dot + recomp_cycle.RHX.Q_dot);

            recomp_cycle.m_dot_turbine = m_dot_t;
            recomp_cycle.conv_tol = m_tol;

            return;
        }


        public void RecompCycle_PCRC_without_Reheating(core luis, ref core.PCRCwithoutReheating cicloPCRC_withoutRH, Double m_W_dot_net,
            Double m_T_mc2_in, Double m_T_t_in, Double P_mc2_in, Double m_P_mc2_out, Double m_P_mc1_in, Double m_T_mc1_in, Double m_P_mc1_out,
            Double UA_LT, Double UA_HT, Double m_eta_mc2, Double m_eta_rc, Double m_eta_mc1, Double m_eta_t, Int64 m_N_sub_hxrs,
            Double m_recomp_frac, Double m_tol, Double eta_thermal2, Double dp2_lt1, Double dp2_lt2, Double dp2_ht1, Double dp2_ht2,
            Double dp2_pc1, Double dp2_pc2, Double dp2_phx1, Double dp2_phx2, Double dp2_cooler1, Double dp2_cooler2)
        {
            int cpp_offset = 1;
            double[] m_temp_last = new double[12];
            double[] m_pres_last = new double[12];
            double[] m_entr_last = new double[12];
            double[] m_enth_last = new double[12];
            double[] m_dens_last = new double[12];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = dp2_ht1;
            m_DP_HT[1] = dp2_ht2;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = dp2_lt1;
            m_DP_LT[1] = dp2_lt2;

            double[] m_DP_PC1 = new double[2];
            m_DP_PC1[1] = dp2_pc1;

            double[] m_DP_PC2 = new double[2];
            m_DP_PC2[1] = dp2_cooler2;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = dp2_phx1;

            int max_iter = 100;

            //	// Set RecompCycle member variable
            //	W_dot_net   = I_W_dot_net;		
            //	conv_tol    = tol;
            //	recomp_frac = I_recomp_frac;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc1 = 0.0;
            double w_mc2 = 0.0;
            double w_rc = 0.0;
            double w_t = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;
            //double m_recomp_frac = 0.25;

            //double m_T_mc1_in = 32 + 273.15; 
            //double m_P_mc1_in = 7400;
            //double m_P_mc1_out = 25000;
            //double m_P_mc2_out = 25000;
            //double m_PR_mc1 = 2.427184466019417;
            //double m_T_t_in = 550 + 273.15;
            //double m_T_mc2_in = 32 + 273.15;
            //double m_UA_rec_total = 10000;
            //double m_LT_frac = 0.5;
            //double m_W_dot_net = 50000;
            //double m_eta_mc1 = 0.89;
            //double m_eta_mc2 = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;
            //double m_tol = 0.00001;
            //Int64 m_N_sub_hxrs = 15;

            m_temp_last[1 - cpp_offset] = m_T_mc2_in;
            m_temp_last[11 - cpp_offset] = m_T_mc1_in;
            m_pres_last[11 - cpp_offset] = m_P_mc1_in;
            m_pres_last[12 - cpp_offset] = m_P_mc1_out;
            //double P_mc2_in = m_P_mc1_out / m_PR_mc1;
            m_pres_last[1 - cpp_offset] = P_mc2_in;
            m_pres_last[2 - cpp_offset] = m_P_mc2_out;
            m_temp_last[6 - cpp_offset] = m_T_t_in;

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_PC1[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[11 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC1[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[11 - cpp_offset] + m_DP_PC1[2 - cpp_offset];                                      // absolute pressure drop specified for precooler

            if (m_DP_PC2[2 - cpp_offset] < 0.0)
                m_pres_last[12 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC2[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[12 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC2[2 - cpp_offset];

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];

            int sub_error_code_1 = 0;
            // Determine the outlet states of the main compressor1 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[11 - cpp_offset], m_pres_last[11 - cpp_offset], m_pres_last[12 - cpp_offset], m_eta_mc1,
                true, ref sub_error_code_1, ref m_enth_last[11 - cpp_offset], ref m_entr_last[11 - cpp_offset], ref m_dens_last[11 - cpp_offset],
                ref m_temp_last[12 - cpp_offset], ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset], ref w_mc1);

            //if (sub_error_code_1 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_1);
            //    return false;
            //}

            int sub_error_code_2 = 0;
            // Determine the outlet states of the main compressor2 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc2,
                true, ref sub_error_code_2, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
                ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset], ref w_mc2);

            //if (sub_error_code_2 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_2);
            //    return false;
            //}

            int sub_error_code_3 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_t,
                false, ref sub_error_code_3, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset], ref w_t);

            //if (sub_error_code_3 != 0)
            //{
            //    m_errors.SetError(23);
            //    m_errors.SetError(sub_error_code_3);
            //    return false;
            //}

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                int sub_error_code_4 = 0;
                calculate_turbomachinery_outlet_nuevo(m_temp_last[2 - cpp_offset], m_pres_last[12 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                    true, ref sub_error_code_4, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5], ref dummy[6], ref w_rc);
            }

            if (w_mc1 + w_mc2 + w_rc + w_t <= 0.0)
            {
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;

            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //property_error_code = CO2_TP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);     // fully define state 8
                working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    return;
                }
                m_enth_last[8 - cpp_offset] = working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;
                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        int sub_error_code_5 = 0;

                        calculate_turbomachinery_outlet_nuevo(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code_5, ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset], ref m_temp_last[10 - cpp_offset],
                            ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset], ref w_rc);

                        if (sub_error_code_5 != 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(12), P=pres(12), error_code=error_code, enth=enth(12), entr=entr(12), dens=dens(12));  // fully define state 12
                        luis.working_fluid.FindStateWithTP(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            //error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[12 - cpp_offset];                 // Assume state(10) is the same as state(12)
                        m_enth_last[12 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[12 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[12 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / (w_mc2 * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_t + w_mc1);           // total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    //property_error_code = CO2_TP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);
                    working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);
                    m_enth_last[9 - cpp_offset] = working_fluid.Enthalpy;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    int sub_error_code_6 = 0;
                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code_6, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code_6 > 0)
                    {
                        if (sub_error_code_6 == 11)     // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset]);
                wmm = working_fluid.MolecularWeight;
                working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);

                if (property_error_code != 0)
                {
                    return;
                }

                m_temp_last[3 - cpp_offset] = working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset]);
                    wmm = working_fluid.MolecularWeight;
                    working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        return;
                    }
                    m_temp_last[4 - cpp_offset] = working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                int sub_error_code_7 = 0;
                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code_7, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code_7 > 0)
                {
                    if (sub_error_code_7 == 1)      // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess <= T8_lower_bound || secant_guess >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset]);
            wmm = working_fluid.MolecularWeight;
            working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            m_temp_last[5 - cpp_offset] = working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = working_fluid.Density;

            // Recompression Cycle
            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            double m_W_dot_net_last = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_mc1 * m_dot_t;
            double m_eta_thermal_last = m_W_dot_net_last / Q_dot_PHX;

            // Set cycle state point properties.
            cicloPCRC_withoutRH.temp = m_temp_last;
            cicloPCRC_withoutRH.pres = m_pres_last;
            cicloPCRC_withoutRH.enth = m_enth_last;
            cicloPCRC_withoutRH.entr = m_entr_last;
            cicloPCRC_withoutRH.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            cicloPCRC_withoutRH.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            cicloPCRC_withoutRH.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(cicloPCRC_withoutRH.LT.C_dot_hot, cicloPCRC_withoutRH.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            cicloPCRC_withoutRH.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            cicloPCRC_withoutRH.LT.UA_design = UA_LT_calc;
            cicloPCRC_withoutRH.LT.UA = UA_LT_calc;
            cicloPCRC_withoutRH.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            cicloPCRC_withoutRH.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            cicloPCRC_withoutRH.LT.m_dot_design[0] = m_dot_mc;
            cicloPCRC_withoutRH.LT.m_dot_design[1] = m_dot_t;
            cicloPCRC_withoutRH.LT.T_c_in = m_temp_last[2 - cpp_offset];
            cicloPCRC_withoutRH.LT.T_h_in = m_temp_last[8 - cpp_offset];
            cicloPCRC_withoutRH.LT.P_c_in = m_pres_last[2 - cpp_offset];
            cicloPCRC_withoutRH.LT.P_h_in = m_pres_last[8 - cpp_offset];
            cicloPCRC_withoutRH.LT.P_c_out = m_pres_last[3 - cpp_offset];
            cicloPCRC_withoutRH.LT.P_h_out = m_pres_last[9 - cpp_offset];
            cicloPCRC_withoutRH.LT.Q_dot = Q_dot_LT;
            cicloPCRC_withoutRH.LT.min_DT = min_DT_LT;
            cicloPCRC_withoutRH.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            cicloPCRC_withoutRH.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            cicloPCRC_withoutRH.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(cicloPCRC_withoutRH.HT.C_dot_hot, cicloPCRC_withoutRH.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            cicloPCRC_withoutRH.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            cicloPCRC_withoutRH.HT.UA_design = UA_HT_calc;
            cicloPCRC_withoutRH.HT.UA = UA_HT_calc;
            cicloPCRC_withoutRH.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            cicloPCRC_withoutRH.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            cicloPCRC_withoutRH.HT.m_dot_design[0] = m_dot_t;
            cicloPCRC_withoutRH.HT.m_dot_design[1] = m_dot_t;
            cicloPCRC_withoutRH.HT.T_c_in = m_temp_last[4 - cpp_offset];
            cicloPCRC_withoutRH.HT.T_h_in = m_temp_last[7 - cpp_offset];
            cicloPCRC_withoutRH.HT.P_c_in = m_pres_last[4 - cpp_offset];
            cicloPCRC_withoutRH.HT.P_h_in = m_pres_last[7 - cpp_offset];
            cicloPCRC_withoutRH.HT.P_c_out = m_pres_last[5 - cpp_offset];
            cicloPCRC_withoutRH.HT.P_h_out = m_pres_last[8 - cpp_offset];
            cicloPCRC_withoutRH.HT.Q_dot = Q_dot_HT;
            cicloPCRC_withoutRH.HT.min_DT = min_DT_HT;
            cicloPCRC_withoutRH.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            cicloPCRC_withoutRH.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            cicloPCRC_withoutRH.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            cicloPCRC_withoutRH.PHX.DP_design2 = 0.0;
            //recomp_cycle%PHX%m_dot_design = [m_dot_t, 0.0_dp]

            cicloPCRC_withoutRH.PC.Q_dot = (m_dot_t * (m_enth_last[9 - cpp_offset] - m_enth_last[11 - cpp_offset]));
            cicloPCRC_withoutRH.PC.DP_design1 = 0.0;
            cicloPCRC_withoutRH.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[11 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            cicloPCRC_withoutRH.COOLER.Q_dot = (m_dot_mc * (m_enth_last[12 - cpp_offset] - m_enth_last[1 - cpp_offset]));
            cicloPCRC_withoutRH.COOLER.DP_design1 = 0.0;
            cicloPCRC_withoutRH.COOLER.DP_design2 = m_pres_last[12 - cpp_offset] - m_pres_last[1 - cpp_offset];

            // Calculate cycle performance metrics.
            cicloPCRC_withoutRH.recomp_frac = m_recomp_frac;

            cicloPCRC_withoutRH.W_dot_net = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_mc1 * m_dot_t;

            cicloPCRC_withoutRH.eta_thermal = cicloPCRC_withoutRH.W_dot_net / (cicloPCRC_withoutRH.PHX.Q_dot);

            cicloPCRC_withoutRH.m_dot_turbine = m_dot_t;
            cicloPCRC_withoutRH.conv_tol = m_tol;
        }

 
        //OK reviewed
        public void RecompCycle_PCRC_without_Reheating_for_Optimization(core luis, ref core.PCRCwithoutReheating cicloPCRC_withoutRH, Double m_W_dot_net,
           Double m_T_mc2_in, Double m_T_t_in, Double P_mc2_in, Double m_P_mc2_out, Double m_P_mc1_in, Double m_T_mc1_in, Double m_P_mc1_out,
           Double LT_fraction, Double UA_Total, Double m_eta_mc2, Double m_eta_rc, Double m_eta_mc1, Double m_eta_t, Int64 m_N_sub_hxrs,
           Double m_recomp_frac, Double m_tol, Double eta_thermal2, Double dp2_lt1, Double dp2_lt2, Double dp2_ht1, Double dp2_ht2,
           Double dp2_pc1, Double dp2_pc2, Double dp2_phx1, Double dp2_phx2, Double dp2_cooler1, Double dp2_cooler2)
        {
            double UA_LT = UA_Total * LT_fraction;
            double UA_HT = UA_Total * (1 - LT_fraction);

            int cpp_offset = 1;
            double[] m_temp_last = new double[12];
            double[] m_pres_last = new double[12];
            double[] m_entr_last = new double[12];
            double[] m_enth_last = new double[12];
            double[] m_dens_last = new double[12];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = dp2_ht1;
            m_DP_HT[1] = dp2_ht2;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = dp2_lt1;
            m_DP_LT[1] = dp2_lt2;

            double[] m_DP_PC1 = new double[2];
            m_DP_PC1[1] = dp2_pc1;

            double[] m_DP_PC2 = new double[2];
            m_DP_PC2[1] = dp2_cooler2;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = dp2_phx1;

            int max_iter = 100;

            //	// Set RecompCycle member variable
            //	W_dot_net   = I_W_dot_net;		
            //	conv_tol    = tol;
            //	recomp_frac = I_recomp_frac;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc1 = 0.0;
            double w_mc2 = 0.0;
            double w_rc = 0.0;
            double w_t = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;
            //double m_recomp_frac = 0.25;

            //double m_T_mc1_in = 32 + 273.15; 
            //double m_P_mc1_in = 7400;
            //double m_P_mc1_out = 25000;
            //double m_P_mc2_out = 25000;
            //double m_PR_mc1 = 2.427184466019417;
            //double m_T_t_in = 550 + 273.15;
            //double m_T_mc2_in = 32 + 273.15;
            //double m_UA_rec_total = 10000;
            //double m_LT_frac = 0.5;
            //double m_W_dot_net = 50000;
            //double m_eta_mc1 = 0.89;
            //double m_eta_mc2 = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;
            //double m_tol = 0.00001;
            //Int64 m_N_sub_hxrs = 15;

            m_temp_last[1 - cpp_offset] = m_T_mc2_in;
            m_temp_last[11 - cpp_offset] = m_T_mc1_in;
            m_pres_last[11 - cpp_offset] = m_P_mc1_in;
            m_pres_last[12 - cpp_offset] = m_P_mc1_out;
            //double P_mc2_in = m_P_mc1_out / m_PR_mc1;
            m_pres_last[1 - cpp_offset] = P_mc2_in;
            m_pres_last[2 - cpp_offset] = m_P_mc2_out;
            m_temp_last[6 - cpp_offset] = m_T_t_in;

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_PC1[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[11 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC1[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[11 - cpp_offset] + m_DP_PC1[2 - cpp_offset];                                      // absolute pressure drop specified for precooler

            if (m_DP_PC2[2 - cpp_offset] < 0.0)
                m_pres_last[12 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC2[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[12 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC2[2 - cpp_offset];

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];

            int sub_error_code_1 = 0;
            // Determine the outlet states of the main compressor1 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[11 - cpp_offset], m_pres_last[11 - cpp_offset], m_pres_last[12 - cpp_offset], m_eta_mc1,
                true, ref sub_error_code_1, ref m_enth_last[11 - cpp_offset], ref m_entr_last[11 - cpp_offset], ref m_dens_last[11 - cpp_offset],
                ref m_temp_last[12 - cpp_offset], ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset], ref w_mc1);

            //if (sub_error_code_1 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_1);
            //    return false;
            //}

            int sub_error_code_2 = 0;
            // Determine the outlet states of the main compressor2 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc2,
                true, ref sub_error_code_2, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
                ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset], ref w_mc2);

            //if (sub_error_code_2 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_2);
            //    return false;
            //}

            int sub_error_code_3 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_t,
                false, ref sub_error_code_3, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset], ref w_t);

            //if (sub_error_code_3 != 0)
            //{
            //    m_errors.SetError(23);
            //    m_errors.SetError(sub_error_code_3);
            //    return false;
            //}

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                int sub_error_code_4 = 0;
                calculate_turbomachinery_outlet_nuevo(m_temp_last[2 - cpp_offset], m_pres_last[12 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                    true, ref sub_error_code_4, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5], ref dummy[6], ref w_rc);
            }

            if (w_mc1 + w_mc2 + w_rc + w_t <= 0.0)
            {
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;

            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //property_error_code = CO2_TP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);     // fully define state 8
                working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    return;
                }
                m_enth_last[8 - cpp_offset] = working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;
                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        int sub_error_code_5 = 0;

                        calculate_turbomachinery_outlet_nuevo(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code_5, ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset], ref m_temp_last[10 - cpp_offset],
                            ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset], ref w_rc);

                        if (sub_error_code_5 != 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(12), P=pres(12), error_code=error_code, enth=enth(12), entr=entr(12), dens=dens(12));  // fully define state 12
                        luis.working_fluid.FindStateWithTP(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            //error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[12 - cpp_offset];                 // Assume state(10) is the same as state(12)
                        m_enth_last[12 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[12 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[12 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / (w_mc2 * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_t + w_mc1);           // total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    //property_error_code = CO2_TP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);
                    working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);
                    m_enth_last[9 - cpp_offset] = working_fluid.Enthalpy;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    int sub_error_code_6 = 0;
                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code_6, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code_6 > 0)
                    {
                        if (sub_error_code_6 == 11)     // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset]);
                wmm = working_fluid.MolecularWeight;
                working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);

                if (property_error_code != 0)
                {
                    return;
                }

                m_temp_last[3 - cpp_offset] = working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset]);
                    wmm = working_fluid.MolecularWeight;
                    working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        return;
                    }
                    m_temp_last[4 - cpp_offset] = working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                int sub_error_code_7 = 0;
                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code_7, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code_7 > 0)
                {
                    if (sub_error_code_7 == 1)      // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess <= T8_lower_bound || secant_guess >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset]);
            wmm = working_fluid.MolecularWeight;
            working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            m_temp_last[5 - cpp_offset] = working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = working_fluid.Density;

            // Recompression Cycle
            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            double m_W_dot_net_last = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_mc1 * m_dot_t;
            double m_eta_thermal_last = m_W_dot_net_last / Q_dot_PHX;

            // Set cycle state point properties.
            cicloPCRC_withoutRH.temp = m_temp_last;
            cicloPCRC_withoutRH.pres = m_pres_last;
            cicloPCRC_withoutRH.enth = m_enth_last;
            cicloPCRC_withoutRH.entr = m_entr_last;
            cicloPCRC_withoutRH.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            cicloPCRC_withoutRH.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            cicloPCRC_withoutRH.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(cicloPCRC_withoutRH.LT.C_dot_hot, cicloPCRC_withoutRH.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            cicloPCRC_withoutRH.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            cicloPCRC_withoutRH.LT.UA_design = UA_LT_calc;
            cicloPCRC_withoutRH.LT.UA = UA_LT_calc;
            cicloPCRC_withoutRH.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            cicloPCRC_withoutRH.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            cicloPCRC_withoutRH.LT.m_dot_design[0] = m_dot_mc;
            cicloPCRC_withoutRH.LT.m_dot_design[1] = m_dot_t;
            cicloPCRC_withoutRH.LT.T_c_in = m_temp_last[2 - cpp_offset];
            cicloPCRC_withoutRH.LT.T_h_in = m_temp_last[8 - cpp_offset];
            cicloPCRC_withoutRH.LT.P_c_in = m_pres_last[2 - cpp_offset];
            cicloPCRC_withoutRH.LT.P_h_in = m_pres_last[8 - cpp_offset];
            cicloPCRC_withoutRH.LT.P_c_out = m_pres_last[3 - cpp_offset];
            cicloPCRC_withoutRH.LT.P_h_out = m_pres_last[9 - cpp_offset];
            cicloPCRC_withoutRH.LT.Q_dot = Q_dot_LT;
            cicloPCRC_withoutRH.LT.min_DT = min_DT_LT;
            cicloPCRC_withoutRH.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            cicloPCRC_withoutRH.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            cicloPCRC_withoutRH.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(cicloPCRC_withoutRH.HT.C_dot_hot, cicloPCRC_withoutRH.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            cicloPCRC_withoutRH.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            cicloPCRC_withoutRH.HT.UA_design = UA_HT_calc;
            cicloPCRC_withoutRH.HT.UA = UA_HT_calc;
            cicloPCRC_withoutRH.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            cicloPCRC_withoutRH.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            cicloPCRC_withoutRH.HT.m_dot_design[0] = m_dot_t;
            cicloPCRC_withoutRH.HT.m_dot_design[1] = m_dot_t;
            cicloPCRC_withoutRH.HT.T_c_in = m_temp_last[4 - cpp_offset];
            cicloPCRC_withoutRH.HT.T_h_in = m_temp_last[7 - cpp_offset];
            cicloPCRC_withoutRH.HT.P_c_in = m_pres_last[4 - cpp_offset];
            cicloPCRC_withoutRH.HT.P_h_in = m_pres_last[7 - cpp_offset];
            cicloPCRC_withoutRH.HT.P_c_out = m_pres_last[5 - cpp_offset];
            cicloPCRC_withoutRH.HT.P_h_out = m_pres_last[8 - cpp_offset];
            cicloPCRC_withoutRH.HT.Q_dot = Q_dot_HT;
            cicloPCRC_withoutRH.HT.min_DT = min_DT_HT;
            cicloPCRC_withoutRH.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            cicloPCRC_withoutRH.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            cicloPCRC_withoutRH.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            cicloPCRC_withoutRH.PHX.DP_design2 = 0.0;
            //recomp_cycle%PHX%m_dot_design = [m_dot_t, 0.0_dp]

            cicloPCRC_withoutRH.PC.Q_dot = (m_dot_t * (m_enth_last[9 - cpp_offset] - m_enth_last[11 - cpp_offset]));
            cicloPCRC_withoutRH.PC.DP_design1 = 0.0;
            cicloPCRC_withoutRH.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[11 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            cicloPCRC_withoutRH.COOLER.Q_dot = (m_dot_mc * (m_enth_last[12 - cpp_offset] - m_enth_last[1 - cpp_offset]));
            cicloPCRC_withoutRH.COOLER.DP_design1 = 0.0;
            cicloPCRC_withoutRH.COOLER.DP_design2 = m_pres_last[12 - cpp_offset] - m_pres_last[1 - cpp_offset];

            // Calculate cycle performance metrics.
            cicloPCRC_withoutRH.recomp_frac = m_recomp_frac;

            cicloPCRC_withoutRH.W_dot_net = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_mc1 * m_dot_t;

            cicloPCRC_withoutRH.eta_thermal = cicloPCRC_withoutRH.W_dot_net / (cicloPCRC_withoutRH.PHX.Q_dot);

            cicloPCRC_withoutRH.m_dot_turbine = m_dot_t;
            cicloPCRC_withoutRH.conv_tol = m_tol;
        }


        //OK reviewed LCE VALIDADA
        public void RecompCycle_PCRC_with_Reheating(core luis, ref core.PCRCwithReheating cicloPCRC_withRH, Double m_W_dot_net,
            Double m_T_mc2_in, Double m_T_t_in, Double m_T_trh_in, Double m_P_trh_in, Double P_mc2_in, Double m_P_mc2_out, Double m_P_mc1_in, Double m_T_mc1_in, Double m_P_mc1_out,
            Double UA_LT, Double UA_HT, Double m_eta_mc2, Double m_eta_rc, Double m_eta_mc1, Double m_eta_t, Double m_eta_trh, Int64 m_N_sub_hxrs,
            Double m_recomp_frac, Double m_tol, Double eta_thermal2, Double dp2_lt1, Double dp2_lt2, Double dp2_ht1, Double dp2_ht2,
            Double dp2_pc1, Double dp2_pc2, Double dp2_phx1, Double dp2_phx2, Double dp2_rhx1, Double dp2_rhx2, Double dp2_cooler1, Double dp2_cooler2)
        {
            int cpp_offset = 1;
            double[] m_temp_last = new double[14];
            double[] m_pres_last = new double[14];
            double[] m_entr_last = new double[14];
            double[] m_enth_last = new double[14];
            double[] m_dens_last = new double[14];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = dp2_ht1;
            m_DP_HT[1] = dp2_ht2;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = dp2_lt1;
            m_DP_LT[1] = dp2_lt2;

            double[] m_DP_PC1 = new double[2];
            m_DP_PC1[1] = dp2_pc1;

            double[] m_DP_PC2 = new double[2];
            m_DP_PC2[1] = dp2_cooler2;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = dp2_phx1;

            double[] m_DP_RHX = new double[2];
            m_DP_RHX[0] = dp2_rhx1;

            int max_iter = 100;

            //	// Set RecompCycle member variable
            //	W_dot_net   = I_W_dot_net;		
            //	conv_tol    = tol;
            //	recomp_frac = I_recomp_frac;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc1 = 0.0;
            double w_mc2 = 0.0;
            double w_rc = 0.0;
            double w_t = 0.0;
            double w_trh = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;
            //double m_recomp_frac = 0.25;

            //double m_T_mc1_in = 32 + 273.15; 
            //double m_P_mc1_in = 7400;
            //double m_P_mc1_out = 25000;
            //double m_P_mc2_out = 25000;
            //double m_PR_mc1 = 2.427184466019417;
            //double m_T_t_in = 550 + 273.15;
            //double m_T_trh_in = 550 + 273.15;
            //double m_P_trh_in = 17400;
            //double m_T_mc2_in = 32 + 273.15;
            //double m_UA_rec_total = 10000;
            //double m_LT_frac = 0.5;
            //double m_W_dot_net = 50000;
            //double m_eta_mc1 = 0.89;
            //double m_eta_mc2 = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;
            //double m_eta_trh = 0.93;
            //double m_tol = 0.00001;
            //Int64 m_N_sub_hxrs = 15;

            m_temp_last[1 - cpp_offset] = m_T_mc2_in;
            m_temp_last[13 - cpp_offset] = m_T_mc1_in;
            m_pres_last[13 - cpp_offset] = m_P_mc1_in;
            m_temp_last[12 - cpp_offset] = m_T_trh_in;
            m_pres_last[12 - cpp_offset] = m_P_trh_in;
            m_pres_last[14 - cpp_offset] = m_P_mc1_out;
            //double P_mc2_in = m_P_mc1_out / m_PR_mc1;
            m_pres_last[1 - cpp_offset] = P_mc2_in;
            m_pres_last[2 - cpp_offset] = m_P_mc2_out;
            m_temp_last[6 - cpp_offset] = m_T_t_in;

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_RHX[1 - cpp_offset] < 0.0)
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] / (1.0 - Math.Abs(m_DP_RHX[1 - cpp_offset])); // relative pressure drop specified for RHX
            else
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] + m_DP_RHX[1 - cpp_offset];                             // absolute pressure drop specified for RHX

            if (m_DP_PC1[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[13 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC1[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[13 - cpp_offset] + m_DP_PC1[2 - cpp_offset];                                      // absolute pressure drop specified for precooler

            if (m_DP_PC2[2 - cpp_offset] < 0.0)
                m_pres_last[14 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC2[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[14 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC2[2 - cpp_offset];

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];

            int sub_error_code_1 = 0;
            // Determine the outlet states of the main compressor1 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[13 - cpp_offset], m_pres_last[13 - cpp_offset], m_pres_last[14 - cpp_offset], m_eta_mc1,
                true, ref sub_error_code_1, ref m_enth_last[13 - cpp_offset], ref m_entr_last[13 - cpp_offset], ref m_dens_last[13 - cpp_offset],
                ref m_temp_last[14 - cpp_offset], ref m_enth_last[14 - cpp_offset], ref m_entr_last[14 - cpp_offset], ref m_dens_last[14 - cpp_offset], ref w_mc1);
        
            int sub_error_code_2 = 0;
            // Determine the outlet states of the main compressor2 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc2,
                true, ref sub_error_code_2, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
                ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset], ref w_mc2);
          
            int sub_error_code_3 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[11 - cpp_offset], m_eta_t,
                false, ref sub_error_code_3, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[11 - cpp_offset], ref m_enth_last[11 - cpp_offset], ref m_entr_last[11 - cpp_offset], ref m_dens_last[11 - cpp_offset], ref w_t);

            int sub_error_code_4 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_trh,
                false, ref sub_error_code_4, ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset], ref w_trh);

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                int sub_error_code_5 = 0;
                calculate_turbomachinery_outlet_nuevo(m_temp_last[14 - cpp_offset], m_pres_last[14 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                    true, ref sub_error_code_5, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5], ref dummy[6], ref w_rc);
            }

            if (w_mc1 + w_mc2 + w_rc + w_t + w_trh <= 0.0)
            {
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;

            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //property_error_code = CO2_TP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);     // fully define state 8
                working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    return;
                }
                m_enth_last[8 - cpp_offset] = working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;
                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        int sub_error_code_77 = 0;

                        calculate_turbomachinery_outlet_nuevo(m_temp_last[14 - cpp_offset], m_pres_last[14 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code_77, ref m_enth_last[14 - cpp_offset], ref m_entr_last[14 - cpp_offset], ref m_dens_last[14 - cpp_offset], ref m_temp_last[10 - cpp_offset],
                            ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset], ref w_rc);

                        if (sub_error_code_77 != 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(12), P=pres(12), error_code=error_code, enth=enth(12), entr=entr(12), dens=dens(12));  // fully define state 12
                        luis.working_fluid.FindStateWithTP(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            //error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[12 - cpp_offset];                 // Assume state(10) is the same as state(12)
                        m_enth_last[12 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[12 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[12 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / (w_mc2 * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_t + w_trh + w_mc1);           // total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    //property_error_code = CO2_TP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                    working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);
                    m_enth_last[9 - cpp_offset] = working_fluid.Enthalpy;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    int sub_error_code_6 = 0;
                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code_6, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code_6 > 0)
                    {
                        if (sub_error_code_6 == 11)     // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset]);
                wmm = working_fluid.MolecularWeight;
                working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);

                if (property_error_code != 0)
                {
                    return;
                }

                m_temp_last[3 - cpp_offset] = working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset]);
                    wmm = working_fluid.MolecularWeight;
                    working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        return;
                    }
                    m_temp_last[4 - cpp_offset] = working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                int sub_error_code_7 = 0;
                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code_7, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code_7 > 0)
                {
                    if (sub_error_code_7 == 1)      // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess <= T8_lower_bound || secant_guess >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset]);
            wmm = working_fluid.MolecularWeight;
            working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            m_temp_last[5 - cpp_offset] = working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = working_fluid.Density;

            // Recompression Cycle
            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            double Q_dot_RHX = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);
            double m_W_dot_net_last = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_trh * m_dot_t + w_mc1 * m_dot_t;
            double m_eta_thermal_last = m_W_dot_net_last / (Q_dot_PHX + Q_dot_RHX);

            // Set cycle state point properties.
            cicloPCRC_withRH.temp = m_temp_last;
            cicloPCRC_withRH.pres = m_pres_last;
            cicloPCRC_withRH.enth = m_enth_last;
            cicloPCRC_withRH.entr = m_entr_last;
            cicloPCRC_withRH.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            cicloPCRC_withRH.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            cicloPCRC_withRH.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(cicloPCRC_withRH.LT.C_dot_hot, cicloPCRC_withRH.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            cicloPCRC_withRH.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            cicloPCRC_withRH.LT.UA_design = UA_LT_calc;
            cicloPCRC_withRH.LT.UA = UA_LT_calc;
            cicloPCRC_withRH.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            cicloPCRC_withRH.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            cicloPCRC_withRH.LT.m_dot_design[0] = m_dot_mc;
            cicloPCRC_withRH.LT.m_dot_design[1] = m_dot_t;
            cicloPCRC_withRH.LT.T_c_in = m_temp_last[2 - cpp_offset];
            cicloPCRC_withRH.LT.T_h_in = m_temp_last[8 - cpp_offset];
            cicloPCRC_withRH.LT.P_c_in = m_pres_last[2 - cpp_offset];
            cicloPCRC_withRH.LT.P_h_in = m_pres_last[8 - cpp_offset];
            cicloPCRC_withRH.LT.P_c_out = m_pres_last[3 - cpp_offset];
            cicloPCRC_withRH.LT.P_h_out = m_pres_last[9 - cpp_offset];
            cicloPCRC_withRH.LT.Q_dot = Q_dot_LT;
            cicloPCRC_withRH.LT.min_DT = min_DT_LT;
            cicloPCRC_withRH.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            cicloPCRC_withRH.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            cicloPCRC_withRH.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(cicloPCRC_withRH.HT.C_dot_hot, cicloPCRC_withRH.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            cicloPCRC_withRH.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            cicloPCRC_withRH.HT.UA_design = UA_HT_calc;
            cicloPCRC_withRH.HT.UA = UA_HT_calc;
            cicloPCRC_withRH.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            cicloPCRC_withRH.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            cicloPCRC_withRH.HT.m_dot_design[0] = m_dot_t;
            cicloPCRC_withRH.HT.m_dot_design[1] = m_dot_t;
            cicloPCRC_withRH.HT.T_c_in = m_temp_last[4 - cpp_offset];
            cicloPCRC_withRH.HT.T_h_in = m_temp_last[7 - cpp_offset];
            cicloPCRC_withRH.HT.P_c_in = m_pres_last[4 - cpp_offset];
            cicloPCRC_withRH.HT.P_h_in = m_pres_last[7 - cpp_offset];
            cicloPCRC_withRH.HT.P_c_out = m_pres_last[5 - cpp_offset];
            cicloPCRC_withRH.HT.P_h_out = m_pres_last[8 - cpp_offset];
            cicloPCRC_withRH.HT.Q_dot = Q_dot_HT;
            cicloPCRC_withRH.HT.min_DT = min_DT_HT;
            cicloPCRC_withRH.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            cicloPCRC_withRH.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            cicloPCRC_withRH.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            cicloPCRC_withRH.PHX.DP_design2 = 0.0;

            cicloPCRC_withRH.RHX.Q_dot = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);
            cicloPCRC_withRH.RHX.DP_design1 = m_pres_last[11 - cpp_offset] - m_pres_last[12 - cpp_offset];
            cicloPCRC_withRH.RHX.DP_design2 = 0.0;

            cicloPCRC_withRH.PC.Q_dot = (m_dot_t * (m_enth_last[9 - cpp_offset] - m_enth_last[13 - cpp_offset]));
            cicloPCRC_withRH.PC.DP_design1 = 0.0;
            cicloPCRC_withRH.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[13 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            cicloPCRC_withRH.COOLER.Q_dot = (m_dot_mc * (m_enth_last[14 - cpp_offset] - m_enth_last[1 - cpp_offset]));
            cicloPCRC_withRH.COOLER.DP_design1 = 0.0;
            cicloPCRC_withRH.COOLER.DP_design2 = m_pres_last[14 - cpp_offset] - m_pres_last[1 - cpp_offset];

            // Calculate cycle performance metrics.
            cicloPCRC_withRH.recomp_frac = m_recomp_frac;

            cicloPCRC_withRH.W_dot_net = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_trh * m_dot_t + w_mc1 * m_dot_t;

            cicloPCRC_withRH.eta_thermal = cicloPCRC_withRH.W_dot_net / (cicloPCRC_withRH.PHX.Q_dot + cicloPCRC_withRH.RHX.Q_dot);

            cicloPCRC_withRH.m_dot_turbine = m_dot_t;
            cicloPCRC_withRH.conv_tol = m_tol;
        }

        //OK reviewed
        public void RecompCycle_PCRC_with_Reheating_for_Optimization(core luis, ref core.PCRCwithReheating cicloPCRC_withRH, Double m_W_dot_net,
            Double m_T_mc2_in, Double m_T_t_in, Double m_T_trh_in, Double m_P_trh_in, Double P_mc2_in, Double m_P_mc2_out, Double m_P_mc1_in, Double m_T_mc1_in, Double m_P_mc1_out,
            Double LT_fraction, Double UA_Total, Double m_eta_mc2, Double m_eta_rc, Double m_eta_mc1, Double m_eta_t, Double m_eta_trh, Int64 m_N_sub_hxrs,
            Double m_recomp_frac, Double m_tol, Double eta_thermal2, Double dp2_lt1, Double dp2_lt2, Double dp2_ht1, Double dp2_ht2,
            Double dp2_pc1, Double dp2_pc2, Double dp2_phx1, Double dp2_phx2, Double dp2_rhx1, Double dp2_rhx2, Double dp2_cooler1, Double dp2_cooler2)
        {
            double UA_LT = UA_Total * LT_fraction;
            double UA_HT = UA_Total * (1 - LT_fraction);

            int cpp_offset = 1;
            double[] m_temp_last = new double[14];
            double[] m_pres_last = new double[14];
            double[] m_entr_last = new double[14];
            double[] m_enth_last = new double[14];
            double[] m_dens_last = new double[14];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = dp2_ht1;
            m_DP_HT[1] = dp2_ht2;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = dp2_lt1;
            m_DP_LT[1] = dp2_lt2;

            double[] m_DP_PC1 = new double[2];
            m_DP_PC1[1] = dp2_pc1;

            double[] m_DP_PC2 = new double[2];
            m_DP_PC2[1] = dp2_cooler2;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = dp2_phx1;

            double[] m_DP_RHX = new double[2];
            m_DP_RHX[0] = dp2_rhx1;

            int max_iter = 100;

            //	// Set RecompCycle member variable
            //	W_dot_net   = I_W_dot_net;		
            //	conv_tol    = tol;
            //	recomp_frac = I_recomp_frac;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc1 = 0.0;
            double w_mc2 = 0.0;
            double w_rc = 0.0;
            double w_t = 0.0;
            double w_trh = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;
            //double m_recomp_frac = 0.25;

            //double m_T_mc1_in = 32 + 273.15; 
            //double m_P_mc1_in = 7400;
            //double m_P_mc1_out = 25000;
            //double m_P_mc2_out = 25000;
            //double m_PR_mc1 = 2.427184466019417;
            //double m_T_t_in = 550 + 273.15;
            //double m_T_trh_in = 550 + 273.15;
            //double m_P_trh_in = 17400;
            //double m_T_mc2_in = 32 + 273.15;
            //double m_UA_rec_total = 10000;
            //double m_LT_frac = 0.5;
            //double m_W_dot_net = 50000;
            //double m_eta_mc1 = 0.89;
            //double m_eta_mc2 = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;
            //double m_eta_trh = 0.93;
            //double m_tol = 0.00001;
            //Int64 m_N_sub_hxrs = 15;

            m_temp_last[1 - cpp_offset] = m_T_mc2_in;
            m_temp_last[13 - cpp_offset] = m_T_mc1_in;
            m_pres_last[13 - cpp_offset] = m_P_mc1_in;
            m_temp_last[12 - cpp_offset] = m_T_trh_in;
            m_pres_last[12 - cpp_offset] = m_P_trh_in;
            m_pres_last[14 - cpp_offset] = m_P_mc1_out;
            //double P_mc2_in = m_P_mc1_out / m_PR_mc1;
            m_pres_last[1 - cpp_offset] = P_mc2_in;
            m_pres_last[2 - cpp_offset] = m_P_mc2_out;
            m_temp_last[6 - cpp_offset] = m_T_t_in;

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_RHX[1 - cpp_offset] < 0.0)
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] + m_pres_last[12 - cpp_offset] * Math.Abs(m_DP_RHX[1 - cpp_offset]); // relative pressure drop specified for RHX
            else
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] + m_DP_RHX[1 - cpp_offset];                             // absolute pressure drop specified for RHX

            if (m_DP_PC1[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[13 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC1[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[13 - cpp_offset] + m_DP_PC1[2 - cpp_offset];                                      // absolute pressure drop specified for precooler

            if (m_DP_PC2[2 - cpp_offset] < 0.0)
                m_pres_last[14 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC2[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[14 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC2[2 - cpp_offset];

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];

            int sub_error_code_1 = 0;
            // Determine the outlet states of the main compressor1 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[13 - cpp_offset], m_pres_last[13 - cpp_offset], m_pres_last[14 - cpp_offset], m_eta_mc1,
                true, ref sub_error_code_1, ref m_enth_last[13 - cpp_offset], ref m_entr_last[13 - cpp_offset], ref m_dens_last[13 - cpp_offset],
                ref m_temp_last[14 - cpp_offset], ref m_enth_last[14 - cpp_offset], ref m_entr_last[14 - cpp_offset], ref m_dens_last[14 - cpp_offset], ref w_mc1);

            int sub_error_code_2 = 0;
            // Determine the outlet states of the main compressor2 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc2,
                true, ref sub_error_code_2, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
                ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset], ref w_mc2);

            int sub_error_code_3 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[11 - cpp_offset], m_eta_t,
                false, ref sub_error_code_3, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[11 - cpp_offset], ref m_enth_last[11 - cpp_offset], ref m_entr_last[11 - cpp_offset], ref m_dens_last[11 - cpp_offset], ref w_t);

            int sub_error_code_4 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_trh,
                false, ref sub_error_code_4, ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset], ref w_trh);

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                int sub_error_code_5 = 0;
                calculate_turbomachinery_outlet_nuevo(m_temp_last[14 - cpp_offset], m_pres_last[14 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                    true, ref sub_error_code_5, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5], ref dummy[6], ref w_rc);
            }

            if (w_mc1 + w_mc2 + w_rc + w_t + w_trh <= 0.0)
            {
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;

            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //property_error_code = CO2_TP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);     // fully define state 8
                working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    return;
                }
                m_enth_last[8 - cpp_offset] = working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;
                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        int sub_error_code_77 = 0;

                        calculate_turbomachinery_outlet_nuevo(m_temp_last[14 - cpp_offset], m_pres_last[14 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code_77, ref m_enth_last[14 - cpp_offset], ref m_entr_last[14 - cpp_offset], ref m_dens_last[14 - cpp_offset], ref m_temp_last[10 - cpp_offset],
                            ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset], ref w_rc);

                        if (sub_error_code_77 != 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(12), P=pres(12), error_code=error_code, enth=enth(12), entr=entr(12), dens=dens(12));  // fully define state 12
                        luis.working_fluid.FindStateWithTP(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            //error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[12 - cpp_offset];                 // Assume state(10) is the same as state(12)
                        m_enth_last[12 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[12 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[12 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / (w_mc2 * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_t + w_trh + w_mc1);           // total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    //property_error_code = CO2_TP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                    working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);
                    m_enth_last[9 - cpp_offset] = working_fluid.Enthalpy;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    int sub_error_code_6 = 0;
                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code_6, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code_6 > 0)
                    {
                        if (sub_error_code_6 == 11)     // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset]);
                wmm = working_fluid.MolecularWeight;
                working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);

                if (property_error_code != 0)
                {
                    return;
                }

                m_temp_last[3 - cpp_offset] = working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset]);
                    wmm = working_fluid.MolecularWeight;
                    working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        return;
                    }
                    m_temp_last[4 - cpp_offset] = working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                int sub_error_code_7 = 0;
                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code_7, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code_7 > 0)
                {
                    if (sub_error_code_7 == 1)      // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess <= T8_lower_bound || secant_guess >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset]);
            wmm = working_fluid.MolecularWeight;
            working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            m_temp_last[5 - cpp_offset] = working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = working_fluid.Density;

            // Recompression Cycle
            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            double Q_dot_RHX = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);
            double m_W_dot_net_last = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_trh * m_dot_t + w_mc1 * m_dot_t;
            double m_eta_thermal_last = m_W_dot_net_last / (Q_dot_PHX + Q_dot_RHX);

            // Set cycle state point properties.
            cicloPCRC_withRH.temp = m_temp_last;
            cicloPCRC_withRH.pres = m_pres_last;
            cicloPCRC_withRH.enth = m_enth_last;
            cicloPCRC_withRH.entr = m_entr_last;
            cicloPCRC_withRH.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            cicloPCRC_withRH.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            cicloPCRC_withRH.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(cicloPCRC_withRH.LT.C_dot_hot, cicloPCRC_withRH.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            cicloPCRC_withRH.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            cicloPCRC_withRH.LT.UA_design = UA_LT_calc;
            cicloPCRC_withRH.LT.UA = UA_LT_calc;
            cicloPCRC_withRH.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            cicloPCRC_withRH.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            cicloPCRC_withRH.LT.m_dot_design[0] = m_dot_mc;
            cicloPCRC_withRH.LT.m_dot_design[1] = m_dot_t;
            cicloPCRC_withRH.LT.T_c_in = m_temp_last[2 - cpp_offset];
            cicloPCRC_withRH.LT.T_h_in = m_temp_last[8 - cpp_offset];
            cicloPCRC_withRH.LT.P_c_in = m_pres_last[2 - cpp_offset];
            cicloPCRC_withRH.LT.P_h_in = m_pres_last[8 - cpp_offset];
            cicloPCRC_withRH.LT.P_c_out = m_pres_last[3 - cpp_offset];
            cicloPCRC_withRH.LT.P_h_out = m_pres_last[9 - cpp_offset];
            cicloPCRC_withRH.LT.Q_dot = Q_dot_LT;
            cicloPCRC_withRH.LT.min_DT = min_DT_LT;
            cicloPCRC_withRH.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            cicloPCRC_withRH.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            cicloPCRC_withRH.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(cicloPCRC_withRH.HT.C_dot_hot, cicloPCRC_withRH.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            cicloPCRC_withRH.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            cicloPCRC_withRH.HT.UA_design = UA_HT_calc;
            cicloPCRC_withRH.HT.UA = UA_HT_calc;
            cicloPCRC_withRH.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            cicloPCRC_withRH.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            cicloPCRC_withRH.HT.m_dot_design[0] = m_dot_t;
            cicloPCRC_withRH.HT.m_dot_design[1] = m_dot_t;
            cicloPCRC_withRH.HT.T_c_in = m_temp_last[4 - cpp_offset];
            cicloPCRC_withRH.HT.T_h_in = m_temp_last[7 - cpp_offset];
            cicloPCRC_withRH.HT.P_c_in = m_pres_last[4 - cpp_offset];
            cicloPCRC_withRH.HT.P_h_in = m_pres_last[7 - cpp_offset];
            cicloPCRC_withRH.HT.P_c_out = m_pres_last[5 - cpp_offset];
            cicloPCRC_withRH.HT.P_h_out = m_pres_last[8 - cpp_offset];
            cicloPCRC_withRH.HT.Q_dot = Q_dot_HT;
            cicloPCRC_withRH.HT.min_DT = min_DT_HT;
            cicloPCRC_withRH.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            cicloPCRC_withRH.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            cicloPCRC_withRH.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            cicloPCRC_withRH.PHX.DP_design2 = 0.0;

            cicloPCRC_withRH.RHX.Q_dot = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);
            cicloPCRC_withRH.RHX.DP_design1 = m_pres_last[11 - cpp_offset] - m_pres_last[12 - cpp_offset];
            cicloPCRC_withRH.RHX.DP_design2 = 0.0;

            cicloPCRC_withRH.PC.Q_dot = (m_dot_t * (m_enth_last[9 - cpp_offset] - m_enth_last[13 - cpp_offset]));
            cicloPCRC_withRH.PC.DP_design1 = 0.0;
            cicloPCRC_withRH.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[13 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            cicloPCRC_withRH.COOLER.Q_dot = (m_dot_mc * (m_enth_last[14 - cpp_offset] - m_enth_last[1 - cpp_offset]));
            cicloPCRC_withRH.COOLER.DP_design1 = 0.0;
            cicloPCRC_withRH.COOLER.DP_design2 = m_pres_last[14 - cpp_offset] - m_pres_last[1 - cpp_offset];

            // Calculate cycle performance metrics.
            cicloPCRC_withRH.recomp_frac = m_recomp_frac;

            cicloPCRC_withRH.W_dot_net = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_trh * m_dot_t + w_mc1 * m_dot_t;

            cicloPCRC_withRH.eta_thermal = cicloPCRC_withRH.W_dot_net / (cicloPCRC_withRH.PHX.Q_dot + cicloPCRC_withRH.RHX.Q_dot);

            cicloPCRC_withRH.m_dot_turbine = m_dot_t;
            cicloPCRC_withRH.conv_tol = m_tol;
        }

        public void RecompCycle_RCMCI_without_Reheating(core luis, ref core.RCMCIwithoutReheating cicloRCMCI_withoutRH, Double m_W_dot_net,
           Double m_T_mc2_in, Double m_T_t_in, Double P_mc2_in, Double m_P_mc2_out, Double m_P_mc1_in, Double m_T_mc1_in, Double m_P_mc1_out,
           Double UA_LT, Double UA_HT, Double m_eta_mc2, Double m_eta_rc, Double m_eta_mc1, Double m_eta_t, Int64 m_N_sub_hxrs,
           Double m_recomp_frac, Double m_tol, Double eta_thermal2, Double dp2_lt1, Double dp2_lt2, Double dp2_ht1, Double dp2_ht2,
           Double dp2_pc1, Double dp2_pc2, Double dp2_phx1, Double dp2_phx2, Double dp2_cooler1, Double dp2_cooler2)
        {
            int cpp_offset = 1;
            double[] m_temp_last = new double[12];
            double[] m_pres_last = new double[12];
            double[] m_entr_last = new double[12];
            double[] m_enth_last = new double[12];
            double[] m_dens_last = new double[12];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = dp2_ht1;
            m_DP_HT[1] = dp2_ht2;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = dp2_lt1;
            m_DP_LT[1] = dp2_lt2;

            double[] m_DP_PC1 = new double[2];
            m_DP_PC1[1] = dp2_pc1;

            double[] m_DP_PC2 = new double[2];
            m_DP_PC2[1] = dp2_cooler2;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = dp2_phx1;

            int max_iter = 100;

            //	// Set RecompCycle member variable
            //	W_dot_net   = I_W_dot_net;		
            //	conv_tol    = tol;
            //	recomp_frac = I_recomp_frac;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc1 = 0.0;
            double w_mc2 = 0.0;
            double w_rc = 0.0;
            double w_t = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;
            //double m_recomp_frac = 0.25;

            //double m_T_mc1_in = 32 + 273.15; 
            //double m_P_mc1_in = 7400;
            //double m_P_mc1_out = 25000;
            //double m_P_mc2_out = 25000;
            //double m_PR_mc1 = 2.427184466019417;
            //double m_T_t_in = 550 + 273.15;
            //double m_T_mc2_in = 32 + 273.15;
            //double m_UA_rec_total = 10000;
            //double m_LT_frac = 0.5;
            //double m_W_dot_net = 50000;
            //double m_eta_mc1 = 0.89;
            //double m_eta_mc2 = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;
            //double m_tol = 0.00001;
            //Int64 m_N_sub_hxrs = 15;

            m_temp_last[1 - cpp_offset] = m_T_mc2_in;
            m_temp_last[11 - cpp_offset] = m_T_mc1_in;
            m_pres_last[11 - cpp_offset] = m_P_mc1_in;
            m_pres_last[12 - cpp_offset] = m_P_mc1_out;
            //double P_mc2_in = m_P_mc1_out / m_PR_mc1;
            m_pres_last[1 - cpp_offset] = P_mc2_in;
            m_pres_last[2 - cpp_offset] = m_P_mc2_out;
            m_temp_last[6 - cpp_offset] = m_T_t_in;

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_PC1[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[11 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC1[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[11 - cpp_offset] + m_DP_PC1[2 - cpp_offset];                                      // absolute pressure drop specified for precooler

            if (m_DP_PC2[2 - cpp_offset] < 0.0)
                m_pres_last[12 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC2[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[12 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC2[2 - cpp_offset];

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];

            int sub_error_code_1 = 0;
            // Determine the outlet states of the main compressor1 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[11 - cpp_offset], m_pres_last[11 - cpp_offset], m_pres_last[12 - cpp_offset], m_eta_mc1,
                true, ref sub_error_code_1, ref m_enth_last[11 - cpp_offset], ref m_entr_last[11 - cpp_offset], ref m_dens_last[11 - cpp_offset],
                ref m_temp_last[12 - cpp_offset], ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset], ref w_mc1);

            //if (sub_error_code_1 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_1);
            //    return false;
            //}

            int sub_error_code_2 = 0;
            // Determine the outlet states of the main compressor2 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc2,
                true, ref sub_error_code_2, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
                ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset], ref w_mc2);

            //if (sub_error_code_2 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_2);
            //    return false;
            //}

            int sub_error_code_3 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_t,
                false, ref sub_error_code_3, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset], ref w_t);

            //if (sub_error_code_3 != 0)
            //{
            //    m_errors.SetError(23);
            //    m_errors.SetError(sub_error_code_3);
            //    return false;
            //}

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                int sub_error_code_4 = 0;
                calculate_turbomachinery_outlet_nuevo(m_temp_last[2 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                    true, ref sub_error_code_4, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5], ref dummy[6], ref w_rc);
            }

            if (w_mc1 + w_mc2 + w_rc + w_t <= 0.0)
            {
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;

            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //property_error_code = CO2_TP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);     // fully define state 8
                working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    return;
                }
                m_enth_last[8 - cpp_offset] = working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;
                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        int sub_error_code_5 = 0;

                        calculate_turbomachinery_outlet_nuevo(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code_5, ref m_enth_last[9 - cpp_offset], ref m_entr_last[9 - cpp_offset], ref m_dens_last[9 - cpp_offset], ref m_temp_last[10 - cpp_offset],
                            ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset], ref w_rc);

                        if (sub_error_code_5 != 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(9), P=pres(9), error_code=error_code, enth=enth(9), entr=entr(9), dens=dens(9));  // fully define state 9
                        luis.working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            //error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[9 - cpp_offset];                 // Assume state(10) is the same as state(9)
                        m_enth_last[9 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[9 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[9 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / ((w_mc1 + w_mc2) * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_t);           // total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    //property_error_code = CO2_TP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                    working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);
                    m_enth_last[9 - cpp_offset] = working_fluid.Enthalpy;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    int sub_error_code_6 = 0;
                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code_6, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code_6 > 0)
                    {
                        if (sub_error_code_6 == 11)     // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset]);
                wmm = working_fluid.MolecularWeight;
                working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);

                if (property_error_code != 0)
                {
                    return;
                }

                m_temp_last[3 - cpp_offset] = working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset]);
                    wmm = working_fluid.MolecularWeight;
                    working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        return;
                    }
                    m_temp_last[4 - cpp_offset] = working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                int sub_error_code_7 = 0;
                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code_7, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code_7 > 0)
                {
                    if (sub_error_code_7 == 1)      // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess <= T8_lower_bound || secant_guess >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset]);
            wmm = working_fluid.MolecularWeight;
            working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            m_temp_last[5 - cpp_offset] = working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = working_fluid.Density;

            // Recompression Cycle
            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            double m_W_dot_net_last = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_mc1 * m_dot_mc;
            double m_eta_thermal_last = m_W_dot_net_last / Q_dot_PHX;

            // Set cycle state point properties.
            cicloRCMCI_withoutRH.temp = m_temp_last;
            cicloRCMCI_withoutRH.pres = m_pres_last;
            cicloRCMCI_withoutRH.enth = m_enth_last;
            cicloRCMCI_withoutRH.entr = m_entr_last;
            cicloRCMCI_withoutRH.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            cicloRCMCI_withoutRH.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            cicloRCMCI_withoutRH.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(cicloRCMCI_withoutRH.LT.C_dot_hot, cicloRCMCI_withoutRH.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            cicloRCMCI_withoutRH.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            cicloRCMCI_withoutRH.LT.UA_design = UA_LT_calc;
            cicloRCMCI_withoutRH.LT.UA = UA_LT_calc;
            cicloRCMCI_withoutRH.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            cicloRCMCI_withoutRH.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            cicloRCMCI_withoutRH.LT.m_dot_design[0] = m_dot_mc;
            cicloRCMCI_withoutRH.LT.m_dot_design[1] = m_dot_t;
            cicloRCMCI_withoutRH.LT.T_c_in = m_temp_last[2 - cpp_offset];
            cicloRCMCI_withoutRH.LT.T_h_in = m_temp_last[8 - cpp_offset];
            cicloRCMCI_withoutRH.LT.P_c_in = m_pres_last[2 - cpp_offset];
            cicloRCMCI_withoutRH.LT.P_h_in = m_pres_last[8 - cpp_offset];
            cicloRCMCI_withoutRH.LT.P_c_out = m_pres_last[3 - cpp_offset];
            cicloRCMCI_withoutRH.LT.P_h_out = m_pres_last[9 - cpp_offset];
            cicloRCMCI_withoutRH.LT.Q_dot = Q_dot_LT;
            cicloRCMCI_withoutRH.LT.min_DT = min_DT_LT;
            cicloRCMCI_withoutRH.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            cicloRCMCI_withoutRH.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            cicloRCMCI_withoutRH.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(cicloRCMCI_withoutRH.HT.C_dot_hot, cicloRCMCI_withoutRH.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            cicloRCMCI_withoutRH.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            cicloRCMCI_withoutRH.HT.UA_design = UA_HT_calc;
            cicloRCMCI_withoutRH.HT.UA = UA_HT_calc;
            cicloRCMCI_withoutRH.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            cicloRCMCI_withoutRH.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            cicloRCMCI_withoutRH.HT.m_dot_design[0] = m_dot_t;
            cicloRCMCI_withoutRH.HT.m_dot_design[1] = m_dot_t;
            cicloRCMCI_withoutRH.HT.T_c_in = m_temp_last[4 - cpp_offset];
            cicloRCMCI_withoutRH.HT.T_h_in = m_temp_last[7 - cpp_offset];
            cicloRCMCI_withoutRH.HT.P_c_in = m_pres_last[4 - cpp_offset];
            cicloRCMCI_withoutRH.HT.P_h_in = m_pres_last[7 - cpp_offset];
            cicloRCMCI_withoutRH.HT.P_c_out = m_pres_last[5 - cpp_offset];
            cicloRCMCI_withoutRH.HT.P_h_out = m_pres_last[8 - cpp_offset];
            cicloRCMCI_withoutRH.HT.Q_dot = Q_dot_HT;
            cicloRCMCI_withoutRH.HT.min_DT = min_DT_HT;
            cicloRCMCI_withoutRH.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            cicloRCMCI_withoutRH.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            cicloRCMCI_withoutRH.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            cicloRCMCI_withoutRH.PHX.DP_design2 = 0.0;
            //recomp_cycle%PHX%m_dot_design = [m_dot_t, 0.0_dp]

            cicloRCMCI_withoutRH.PC.Q_dot = (m_dot_t * (m_enth_last[9 - cpp_offset] - m_enth_last[11 - cpp_offset]));
            cicloRCMCI_withoutRH.PC.DP_design1 = 0.0;
            cicloRCMCI_withoutRH.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[11 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            cicloRCMCI_withoutRH.COOLER.Q_dot = (m_dot_mc * (m_enth_last[12 - cpp_offset] - m_enth_last[1 - cpp_offset]));
            cicloRCMCI_withoutRH.COOLER.DP_design1 = 0.0;
            cicloRCMCI_withoutRH.COOLER.DP_design2 = m_pres_last[12 - cpp_offset] - m_pres_last[1 - cpp_offset];

            // Calculate cycle performance metrics.
            cicloRCMCI_withoutRH.recomp_frac = m_recomp_frac;

            cicloRCMCI_withoutRH.W_dot_net = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_mc1 * m_dot_mc;

            cicloRCMCI_withoutRH.eta_thermal = cicloRCMCI_withoutRH.W_dot_net / (cicloRCMCI_withoutRH.PHX.Q_dot);

            cicloRCMCI_withoutRH.m_dot_turbine = m_dot_t;
            cicloRCMCI_withoutRH.conv_tol = m_tol;
        }

 
        public void RecompCycle_RCMCI_without_Reheating_for_Optimization(core luis, ref core.RCMCIwithoutReheating cicloRCMCI_withoutRH, Double m_W_dot_net,
          Double m_T_mc2_in, Double m_T_t_in, Double P_mc2_in, Double m_P_mc2_out, Double m_P_mc1_in, Double m_T_mc1_in, Double m_P_mc1_out,
          Double LT_fraction, Double UA_Total, Double m_eta_mc2, Double m_eta_rc, Double m_eta_mc1, Double m_eta_t, Int64 m_N_sub_hxrs,
          Double m_recomp_frac, Double m_tol, Double eta_thermal2, Double dp2_lt1, Double dp2_lt2, Double dp2_ht1, Double dp2_ht2,
          Double dp2_pc1, Double dp2_pc2, Double dp2_phx1, Double dp2_phx2, Double dp2_cooler1, Double dp2_cooler2)
        {
            double UA_LT = UA_Total * LT_fraction;
            double UA_HT = UA_Total * (1 - LT_fraction);

            int cpp_offset = 1;
            double[] m_temp_last = new double[12];
            double[] m_pres_last = new double[12];
            double[] m_entr_last = new double[12];
            double[] m_enth_last = new double[12];
            double[] m_dens_last = new double[12];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = dp2_ht1;
            m_DP_HT[1] = dp2_ht2;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = dp2_lt1;
            m_DP_LT[1] = dp2_lt2;

            double[] m_DP_PC1 = new double[2];
            m_DP_PC1[1] = dp2_pc1;

            double[] m_DP_PC2 = new double[2];
            m_DP_PC2[1] = dp2_cooler2;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = dp2_phx1;

            int max_iter = 100;

            //	// Set RecompCycle member variable
            //	W_dot_net   = I_W_dot_net;		
            //	conv_tol    = tol;
            //	recomp_frac = I_recomp_frac;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc1 = 0.0;
            double w_mc2 = 0.0;
            double w_rc = 0.0;
            double w_t = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;
            //double m_recomp_frac = 0.25;

            //double m_T_mc1_in = 32 + 273.15; 
            //double m_P_mc1_in = 7400;
            //double m_P_mc1_out = 25000;
            //double m_P_mc2_out = 25000;
            //double m_PR_mc1 = 2.427184466019417;
            //double m_T_t_in = 550 + 273.15;
            //double m_T_mc2_in = 32 + 273.15;
            //double m_UA_rec_total = 10000;
            //double m_LT_frac = 0.5;
            //double m_W_dot_net = 50000;
            //double m_eta_mc1 = 0.89;
            //double m_eta_mc2 = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;
            //double m_tol = 0.00001;
            //Int64 m_N_sub_hxrs = 15;

            m_temp_last[1 - cpp_offset] = m_T_mc2_in;
            m_temp_last[11 - cpp_offset] = m_T_mc1_in;
            m_pres_last[11 - cpp_offset] = m_P_mc1_in;
            m_pres_last[12 - cpp_offset] = m_P_mc1_out;
            //double P_mc2_in = m_P_mc1_out / m_PR_mc1;
            m_pres_last[1 - cpp_offset] = P_mc2_in;
            m_pres_last[2 - cpp_offset] = m_P_mc2_out;
            m_temp_last[6 - cpp_offset] = m_T_t_in;

            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_PC1[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[11 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC1[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[11 - cpp_offset] + m_DP_PC1[2 - cpp_offset];                                      // absolute pressure drop specified for precooler

            if (m_DP_PC2[2 - cpp_offset] < 0.0)
                m_pres_last[12 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC2[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[12 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC2[2 - cpp_offset];

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];

            int sub_error_code_1 = 0;
            // Determine the outlet states of the main compressor1 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[11 - cpp_offset], m_pres_last[11 - cpp_offset], m_pres_last[12 - cpp_offset], m_eta_mc1,
                true, ref sub_error_code_1, ref m_enth_last[11 - cpp_offset], ref m_entr_last[11 - cpp_offset], ref m_dens_last[11 - cpp_offset],
                ref m_temp_last[12 - cpp_offset], ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset], ref w_mc1);

            //if (sub_error_code_1 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_1);
            //    return false;
            //}

            int sub_error_code_2 = 0;
            // Determine the outlet states of the main compressor2 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc2,
                true, ref sub_error_code_2, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
                ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset], ref w_mc2);

            //if (sub_error_code_2 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_2);
            //    return false;
            //}

            int sub_error_code_3 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_t,
                false, ref sub_error_code_3, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset], ref w_t);

            //if (sub_error_code_3 != 0)
            //{
            //    m_errors.SetError(23);
            //    m_errors.SetError(sub_error_code_3);
            //    return false;
            //}

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                int sub_error_code_4 = 0;
                calculate_turbomachinery_outlet_nuevo(m_temp_last[2 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                    true, ref sub_error_code_4, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5], ref dummy[6], ref w_rc);
            }

            if (w_mc1 + w_mc2 + w_rc + w_t <= 0.0)
            {
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;

            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //property_error_code = CO2_TP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);     // fully define state 8
                working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    return;
                }
                m_enth_last[8 - cpp_offset] = working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;
                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        int sub_error_code_5 = 0;

                        calculate_turbomachinery_outlet_nuevo(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code_5, ref m_enth_last[9 - cpp_offset], ref m_entr_last[9 - cpp_offset], ref m_dens_last[9 - cpp_offset], ref m_temp_last[10 - cpp_offset],
                            ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset], ref w_rc);

                        if (sub_error_code_5 != 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(9), P=pres(9), error_code=error_code, enth=enth(9), entr=entr(9), dens=dens(9));  // fully define state 9
                        luis.working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            //error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[9 - cpp_offset];                 // Assume state(10) is the same as state(9)
                        m_enth_last[9 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[9 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[9 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / ((w_mc1 + w_mc2) * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_t);           // total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    //property_error_code = CO2_TP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                    working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);
                    m_enth_last[9 - cpp_offset] = working_fluid.Enthalpy;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    int sub_error_code_6 = 0;
                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code_6, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code_6 > 0)
                    {
                        if (sub_error_code_6 == 11)     // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset]);
                wmm = working_fluid.MolecularWeight;
                working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);

                if (property_error_code != 0)
                {
                    return;
                }

                m_temp_last[3 - cpp_offset] = working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset]);
                    wmm = working_fluid.MolecularWeight;
                    working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        return;
                    }
                    m_temp_last[4 - cpp_offset] = working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                int sub_error_code_7 = 0;
                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code_7, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code_7 > 0)
                {
                    if (sub_error_code_7 == 1)      // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess <= T8_lower_bound || secant_guess >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset]);
            wmm = working_fluid.MolecularWeight;
            working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            m_temp_last[5 - cpp_offset] = working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = working_fluid.Density;

            // Recompression Cycle
            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            double m_W_dot_net_last = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_mc1 * m_dot_mc;
            double m_eta_thermal_last = m_W_dot_net_last / Q_dot_PHX;

            // Set cycle state point properties.
            cicloRCMCI_withoutRH.temp = m_temp_last;
            cicloRCMCI_withoutRH.pres = m_pres_last;
            cicloRCMCI_withoutRH.enth = m_enth_last;
            cicloRCMCI_withoutRH.entr = m_entr_last;
            cicloRCMCI_withoutRH.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            cicloRCMCI_withoutRH.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            cicloRCMCI_withoutRH.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(cicloRCMCI_withoutRH.LT.C_dot_hot, cicloRCMCI_withoutRH.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            cicloRCMCI_withoutRH.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            cicloRCMCI_withoutRH.LT.UA_design = UA_LT_calc;
            cicloRCMCI_withoutRH.LT.UA = UA_LT_calc;
            cicloRCMCI_withoutRH.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            cicloRCMCI_withoutRH.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            cicloRCMCI_withoutRH.LT.m_dot_design[0] = m_dot_mc;
            cicloRCMCI_withoutRH.LT.m_dot_design[1] = m_dot_t;
            cicloRCMCI_withoutRH.LT.T_c_in = m_temp_last[2 - cpp_offset];
            cicloRCMCI_withoutRH.LT.T_h_in = m_temp_last[8 - cpp_offset];
            cicloRCMCI_withoutRH.LT.P_c_in = m_pres_last[2 - cpp_offset];
            cicloRCMCI_withoutRH.LT.P_h_in = m_pres_last[8 - cpp_offset];
            cicloRCMCI_withoutRH.LT.P_c_out = m_pres_last[3 - cpp_offset];
            cicloRCMCI_withoutRH.LT.P_h_out = m_pres_last[9 - cpp_offset];
            cicloRCMCI_withoutRH.LT.Q_dot = Q_dot_LT;
            cicloRCMCI_withoutRH.LT.min_DT = min_DT_LT;
            cicloRCMCI_withoutRH.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            cicloRCMCI_withoutRH.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            cicloRCMCI_withoutRH.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(cicloRCMCI_withoutRH.HT.C_dot_hot, cicloRCMCI_withoutRH.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            cicloRCMCI_withoutRH.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            cicloRCMCI_withoutRH.HT.UA_design = UA_HT_calc;
            cicloRCMCI_withoutRH.HT.UA = UA_HT_calc;
            cicloRCMCI_withoutRH.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            cicloRCMCI_withoutRH.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            cicloRCMCI_withoutRH.HT.m_dot_design[0] = m_dot_t;
            cicloRCMCI_withoutRH.HT.m_dot_design[1] = m_dot_t;
            cicloRCMCI_withoutRH.HT.T_c_in = m_temp_last[4 - cpp_offset];
            cicloRCMCI_withoutRH.HT.T_h_in = m_temp_last[7 - cpp_offset];
            cicloRCMCI_withoutRH.HT.P_c_in = m_pres_last[4 - cpp_offset];
            cicloRCMCI_withoutRH.HT.P_h_in = m_pres_last[7 - cpp_offset];
            cicloRCMCI_withoutRH.HT.P_c_out = m_pres_last[5 - cpp_offset];
            cicloRCMCI_withoutRH.HT.P_h_out = m_pres_last[8 - cpp_offset];
            cicloRCMCI_withoutRH.HT.Q_dot = Q_dot_HT;
            cicloRCMCI_withoutRH.HT.min_DT = min_DT_HT;
            cicloRCMCI_withoutRH.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            cicloRCMCI_withoutRH.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            cicloRCMCI_withoutRH.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            cicloRCMCI_withoutRH.PHX.DP_design2 = 0.0;
            //recomp_cycle%PHX%m_dot_design = [m_dot_t, 0.0_dp]

            cicloRCMCI_withoutRH.PC.Q_dot = (m_dot_t * (m_enth_last[9 - cpp_offset] - m_enth_last[11 - cpp_offset]));
            cicloRCMCI_withoutRH.PC.DP_design1 = 0.0;
            cicloRCMCI_withoutRH.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[11 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            cicloRCMCI_withoutRH.COOLER.Q_dot = (m_dot_mc * (m_enth_last[12 - cpp_offset] - m_enth_last[1 - cpp_offset]));
            cicloRCMCI_withoutRH.COOLER.DP_design1 = 0.0;
            cicloRCMCI_withoutRH.COOLER.DP_design2 = m_pres_last[12 - cpp_offset] - m_pres_last[1 - cpp_offset];

            // Calculate cycle performance metrics.
            cicloRCMCI_withoutRH.recomp_frac = m_recomp_frac;

            cicloRCMCI_withoutRH.W_dot_net = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_mc1 * m_dot_mc;

            cicloRCMCI_withoutRH.eta_thermal = cicloRCMCI_withoutRH.W_dot_net / (cicloRCMCI_withoutRH.PHX.Q_dot);

            cicloRCMCI_withoutRH.m_dot_turbine = m_dot_t;
            cicloRCMCI_withoutRH.conv_tol = m_tol;
        }
        
        public void RecompCycle_RCMCI_with_Reheating(core luis, ref core.RCMCIwithReheating cicloRCMCI_withRH, Double m_W_dot_net,
           Double m_T_mc2_in, Double m_T_t_in, Double m_T_trh_in, Double m_P_trh_in, Double P_mc2_in, Double m_P_mc2_out, Double m_P_mc1_in, Double m_T_mc1_in, Double m_P_mc1_out,
           Double UA_LT, Double UA_HT, Double m_eta_mc2, Double m_eta_rc, Double m_eta_mc1, Double m_eta_t, Double m_eta_trh, Int64 m_N_sub_hxrs,
           Double m_recomp_frac, Double m_tol, Double eta_thermal2, Double dp2_lt1, Double dp2_lt2, Double dp2_ht1, Double dp2_ht2,
           Double dp2_pc1, Double dp2_pc2, Double dp2_phx1, Double dp2_phx2, Double dp2_rhx1, Double dp2_rhx2, Double dp2_cooler1, Double dp2_cooler2)
        {
            int cpp_offset = 1;
            double[] m_temp_last = new double[14];
            double[] m_pres_last = new double[14];
            double[] m_entr_last = new double[14];
            double[] m_enth_last = new double[14];
            double[] m_dens_last = new double[14];

            double[] m_DP_HT = new double[2];
            m_DP_HT[0] = dp2_ht1;
            m_DP_HT[1] = dp2_ht2;

            double[] m_DP_LT = new double[2];
            m_DP_LT[0] = dp2_lt1;
            m_DP_LT[1] = dp2_lt2;

            double[] m_DP_PC1 = new double[2];
            m_DP_PC1[1] = dp2_pc1;

            double[] m_DP_PC2 = new double[2];
            m_DP_PC2[1] = dp2_cooler1;

            double[] m_DP_PHX = new double[2];
            m_DP_PHX[0] = dp2_phx1;

            double[] m_DP_RHX = new double[2];
            m_DP_RHX[0] = dp2_rhx1;

            int max_iter = 100;

            //	// Set RecompCycle member variable
            //	W_dot_net   = I_W_dot_net;		
            //	conv_tol    = tol;
            //	recomp_frac = I_recomp_frac;

            // Set other variables that need to reported at end of this function
            double min_DT_LT = 0.0;
            double min_DT_HT = 0.0;

            double m_dot_t = 0.0;
            double m_dot_mc = 0.0;
            double m_dot_rc = 0.0;
            double w_mc1 = 0.0;
            double w_mc2 = 0.0;
            double w_rc = 0.0;
            double w_t = 0.0;
            double w_trh = 0.0;
            double Q_dot_LT = 0.0;
            double Q_dot_HT = 0.0;
            double UA_LT_calc = 0.0;
            double UA_HT_calc = 0.0;
            //double m_recomp_frac = 0.25;

            //double m_T_mc1_in = 32 + 273.15; 
            //double m_P_mc1_in = 7400;
            //double m_P_mc1_out = 25000;
            //double m_P_mc2_out = 25000;
            //double m_PR_mc1 = 2.427184466019417;
            //double m_T_t_in = 550 + 273.15;
            //double m_T_mc2_in = 32 + 273.15;
            //double m_UA_rec_total = 10000;
            //double m_LT_frac = 0.5;
            //double m_W_dot_net = 50000;
            //double m_eta_mc1 = 0.89;
            //double m_eta_mc2 = 0.89;
            //double m_eta_rc = 0.89;
            //double m_eta_t = 0.93;
            //double m_tol = 0.00001;
            //Int64 m_N_sub_hxrs = 15;

            m_temp_last[1 - cpp_offset] = m_T_mc2_in;
            m_temp_last[6 - cpp_offset] = m_T_t_in;
            m_temp_last[13 - cpp_offset] = m_T_mc1_in;
            m_pres_last[13 - cpp_offset] = m_P_mc1_in;
            m_pres_last[14 - cpp_offset] = m_P_mc1_out;
            //double P_mc2_in = m_P_mc1_out / m_PR_mc1;
            m_pres_last[1 - cpp_offset] = P_mc2_in;
            m_pres_last[2 - cpp_offset] = m_P_mc2_out;
            m_temp_last[12 - cpp_offset] = m_T_trh_in;
            m_pres_last[12 - cpp_offset] = m_P_trh_in;


            // Apply pressure drops to heat exchangers, fully defining the pressures at all stages
            if (m_DP_LT[1 - cpp_offset] < 0.0)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_pres_last[2 - cpp_offset] * Math.Abs(m_DP_LT[1 - cpp_offset]);     // Relative pressure drop specified for LT recuperator (cold stream)
            else
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset] - m_DP_LT[1 - cpp_offset];                                    // Absolute pressure drop specified for LT recuperator (cold stream)

            //double UA_LT = m_UA_rec_total * m_LT_frac;
            //double UA_HT = m_UA_rec_total * (1 - m_LT_frac);

            if (UA_LT < 1E-12)
                m_pres_last[3 - cpp_offset] = m_pres_last[2 - cpp_offset];      // if there is no LT recuperator, there is no pressure drop

            m_pres_last[4 - cpp_offset] = m_pres_last[3 - cpp_offset];          // No pressure drop in mixing value
            m_pres_last[10 - cpp_offset] = m_pres_last[3 - cpp_offset];         // No pressure drop in mixing value

            if (m_DP_HT[1 - cpp_offset] < 0.0)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_pres_last[4 - cpp_offset] * Math.Abs(m_DP_HT[1 - cpp_offset]); // relative pressure drop specified for HT recuperator (cold stream)
            else
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset] - m_DP_HT[1 - cpp_offset];                                // absolute pressure drop specified for HT recuperator (cold stream)

            if (UA_HT < 1E-12)
                m_pres_last[5 - cpp_offset] = m_pres_last[4 - cpp_offset];      // if there is no HT recuperator, there is no pressure drop

            if (m_DP_PHX[1 - cpp_offset] < 0.0)
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_pres_last[5 - cpp_offset] * Math.Abs(m_DP_PHX[1 - cpp_offset]);    // relative pressure drop specified for PHX
            else
                m_pres_last[6 - cpp_offset] = m_pres_last[5 - cpp_offset] - m_DP_PHX[1 - cpp_offset];                               // absolute pressure drop specified for PHX

            if (m_DP_RHX[1 - cpp_offset] < 0.0)
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] / (1.0 - Math.Abs(m_DP_RHX[1 - cpp_offset]));    // relative pressure drop specified for RHX
            else
                m_pres_last[11 - cpp_offset] = m_pres_last[12 - cpp_offset] + m_DP_RHX[1 - cpp_offset];                             // absolute pressure drop specified for RHX

            if (m_DP_PC1[2 - cpp_offset] < 0.0)
                m_pres_last[9 - cpp_offset] = m_pres_last[13 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC1[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[9 - cpp_offset] = m_pres_last[13 - cpp_offset] + m_DP_PC1[2 - cpp_offset];                                      // absolute pressure drop specified for precooler

            if (m_DP_PC2[2 - cpp_offset] < 0.0)
                m_pres_last[14 - cpp_offset] = m_pres_last[1 - cpp_offset] / (1.0 - Math.Abs(m_DP_PC2[2 - cpp_offset]));         // relative pressure drop specified for precooler [P1 = P9 - P9*rel_DP => P1 = P9*(1-rel_DP)
            else
                m_pres_last[14 - cpp_offset] = m_pres_last[1 - cpp_offset] + m_DP_PC2[2 - cpp_offset];

            if (m_DP_LT[2 - cpp_offset] < 0.0)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] / (1.0 - Math.Abs(m_DP_LT[2 - cpp_offset]));           // relative pressure drop specified for LT recuperator (hot stream)
            else
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset] + m_DP_LT[2 - cpp_offset];                        // absolute pressure drop specified for LT recuperator (hot stream)

            if (UA_LT < 1E-12)
                m_pres_last[8 - cpp_offset] = m_pres_last[9 - cpp_offset];      // if there is no LT recup, there is no pressure drop

            if (m_DP_HT[2 - cpp_offset] < 0.0)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] / (1.0 - Math.Abs(m_DP_HT[2 - cpp_offset]));           // relative pressure drop specified for HT recup
            else
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset] + m_DP_HT[2 - cpp_offset];                        // absolute pressure drop specified for HT recup

            if (UA_HT < 1E-12)
                m_pres_last[7 - cpp_offset] = m_pres_last[8 - cpp_offset];

            int sub_error_code_1 = 0;
            // Determine the outlet states of the main compressor1 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[13 - cpp_offset], m_pres_last[13 - cpp_offset], m_pres_last[14 - cpp_offset], m_eta_mc1,
                true, ref sub_error_code_1, ref m_enth_last[13 - cpp_offset], ref m_entr_last[13 - cpp_offset], ref m_dens_last[13 - cpp_offset],
                ref m_temp_last[14 - cpp_offset], ref m_enth_last[14 - cpp_offset], ref m_entr_last[14 - cpp_offset], ref m_dens_last[14 - cpp_offset], ref w_mc1);

            //if (sub_error_code_1 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_1);
            //    return false;
            //}

            int sub_error_code_2 = 0;
            // Determine the outlet states of the main compressor2 and turbine and their specific works
            calculate_turbomachinery_outlet_nuevo(m_temp_last[1 - cpp_offset], m_pres_last[1 - cpp_offset], m_pres_last[2 - cpp_offset], m_eta_mc2,
                true, ref sub_error_code_2, ref m_enth_last[1 - cpp_offset], ref m_entr_last[1 - cpp_offset], ref m_dens_last[1 - cpp_offset],
                ref m_temp_last[2 - cpp_offset], ref m_enth_last[2 - cpp_offset], ref m_entr_last[2 - cpp_offset], ref m_dens_last[2 - cpp_offset], ref w_mc2);

            //if (sub_error_code_2 != 0)
            //{
            //    m_errors.SetError(22);
            //    m_errors.SetError(sub_error_code_2);
            //    return false;
            //}

            int sub_error_code_3 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[6 - cpp_offset], m_pres_last[6 - cpp_offset], m_pres_last[11 - cpp_offset], m_eta_t,
                false, ref sub_error_code_3, ref m_enth_last[6 - cpp_offset], ref m_entr_last[6 - cpp_offset], ref m_dens_last[6 - cpp_offset],
                ref m_temp_last[11 - cpp_offset], ref m_enth_last[11 - cpp_offset], ref m_entr_last[11 - cpp_offset], ref m_dens_last[11 - cpp_offset], ref w_t);

            int sub_error_code_4 = 0;
            calculate_turbomachinery_outlet_nuevo(m_temp_last[12 - cpp_offset], m_pres_last[12 - cpp_offset], m_pres_last[7 - cpp_offset], m_eta_trh,
                false, ref sub_error_code_4, ref m_enth_last[12 - cpp_offset], ref m_entr_last[12 - cpp_offset], ref m_dens_last[12 - cpp_offset],
                ref m_temp_last[7 - cpp_offset], ref m_enth_last[7 - cpp_offset], ref m_entr_last[7 - cpp_offset], ref m_dens_last[7 - cpp_offset], ref w_trh);


            //if (sub_error_code_3 != 0)
            //{
            //    m_errors.SetError(23);
            //    m_errors.SetError(sub_error_code_3);
            //    return false;
            //}

            // Check to ensure this cycle can produce power under the best conditions(ie, temp(9) = temp(2) if there is a recompressing compressor).
            w_rc = 0.0;

            if (m_recomp_frac >= 1E-12)
            {
                double[] dummy = new double[7];

                int sub_error_code_5 = 0;
                calculate_turbomachinery_outlet_nuevo(m_temp_last[2 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                    true, ref sub_error_code_5, ref dummy[0], ref dummy[1], ref dummy[2], ref dummy[3], ref dummy[4], ref dummy[5], ref dummy[6], ref w_rc);
            }

            if (w_mc1 + w_mc2 + w_rc + w_t + w_trh <= 0.0)
            {
                return;
            }

            // Outer iteration loop : temp(8), checking against UA_HT
            double T8_lower_bound = 0.0;
            double T8_upper_bound = 0.0;
            double last_HT_residual = 0.0;
            double last_T8_guess = 0.0;
            if (UA_HT < 1.0E-12)            // No high-temp recuperator
            {
                T8_lower_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // No iteration necessary
                m_temp_last[8 - cpp_offset] = m_temp_last[7 - cpp_offset];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }
            else
            {
                T8_lower_bound = m_temp_last[2 - cpp_offset];       // The lower possible value of temp(8)
                T8_upper_bound = m_temp_last[7 - cpp_offset];       // The highest possible value of temp(8)
                m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // Bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;                   // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT-0
                last_T8_guess = m_temp_last[7 - cpp_offset];
            }

            int property_error_code = 0;
            int T8_iter = 0;

            // T8_loop
            for (T8_iter = 1; T8_iter <= max_iter; T8_iter++)
            {
                //property_error_code = CO2_TP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);     // fully define state 8
                working_fluid.FindStateWithTP(m_temp_last[8 - cpp_offset], m_pres_last[8 - cpp_offset]);

                if (property_error_code != 0)
                {
                    return;
                }
                m_enth_last[8 - cpp_offset] = working_fluid.Enthalpy;
                m_entr_last[8 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[8 - cpp_offset] = working_fluid.Density;

                // Inner iteration loop: temp(9), checking against UA_LT
                double T9_lower_bound, T9_upper_bound, last_LT_residual, last_T9_guess;
                T9_lower_bound = T9_upper_bound = last_LT_residual = last_T9_guess = 0.0;
                if (UA_LT < 1E-12)   // no low-temp recuperator
                {
                    T9_lower_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    T9_upper_bound = m_temp_last[8 - cpp_offset];           // no iteration necessary
                    m_temp_last[9 - cpp_offset] = m_temp_last[8 - cpp_offset];
                    UA_LT_calc = 0.0;
                    last_LT_residual = 0.0;
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }
                else
                {
                    T9_lower_bound = m_temp_last[2 - cpp_offset];       // the lower possible value for T9
                    T9_upper_bound = m_temp_last[8 - cpp_offset];       // the highest possible value for T9
                    m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;  // bisect bounds for first guess
                    UA_LT_calc = -1.0;
                    last_LT_residual = UA_LT;       // know a priori that with T9=T8, UA_calc = 0 therefore residual is UA_LT - 0
                    last_T9_guess = m_temp_last[8 - cpp_offset];
                }

                // T9_loop
                int T9_iter = 0;
                for (T9_iter = 1; T9_iter <= max_iter; T9_iter++)
                {
                    // Determine the outlet state of the recompressor and its specific work
                    if (m_recomp_frac >= 1E-12)
                    {
                        int sub_error_code_5 = 0;

                        calculate_turbomachinery_outlet_nuevo(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset], m_pres_last[10 - cpp_offset], m_eta_rc,
                            true, ref sub_error_code_5, ref m_enth_last[9 - cpp_offset], ref m_entr_last[9 - cpp_offset], ref m_dens_last[9 - cpp_offset], ref m_temp_last[10 - cpp_offset],
                            ref m_enth_last[10 - cpp_offset], ref m_entr_last[10 - cpp_offset], ref m_dens_last[10 - cpp_offset], ref w_rc);

                        if (sub_error_code_5 != 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        w_rc = 0.0;     // the recompressor does not exist

                        //call CO2_TP(T=temp(9), P=pres(9), error_code=error_code, enth=enth(9), entr=entr(9), dens=dens(9));  // fully define state 9
                        luis.working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                        if (property_error_code != 0)
                        {
                            //error_code = 28;
                            return;
                        }
                        m_temp_last[10 - cpp_offset] = m_temp_last[9 - cpp_offset];                 // Assume state(10) is the same as state(9)
                        m_enth_last[9 - cpp_offset] = m_enth_last[10 - cpp_offset] = luis.working_fluid.Enthalpy;
                        m_entr_last[9 - cpp_offset] = m_entr_last[10 - cpp_offset] = luis.working_fluid.Entropy;
                        m_dens_last[9 - cpp_offset] = m_dens_last[10 - cpp_offset] = luis.working_fluid.Density;
                    }

                    // Knowing the specific work of the the recompressing compressor, the required mass flow rate can be determined.
                    m_dot_t = m_W_dot_net / ((w_mc1 + w_mc2) * (1.0 - m_recomp_frac) + w_rc * m_recomp_frac + w_t + w_trh);           // total mass flow rate(through turbine)
                    if (m_dot_t < 0.0)              // positive power output is not possible with these inputs
                    {
                        return;
                    }
                    m_dot_rc = m_dot_t * m_recomp_frac;
                    m_dot_mc = m_dot_t - m_dot_rc;

                    //property_error_code = CO2_TP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);

                    working_fluid.FindStateWithTP(m_temp_last[9 - cpp_offset], m_pres_last[9 - cpp_offset]);
                    m_enth_last[9 - cpp_offset] = working_fluid.Enthalpy;

                    // Calculate the UA value of the low-temperature recuperator.
                    if (UA_LT < 1E-12)           // no low-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                        Q_dot_LT = 0.0;
                    else
                        Q_dot_LT = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]);

                    int sub_error_code_6 = 0;
                    calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_LT, m_dot_mc, m_dot_t, m_temp_last[2 - cpp_offset], m_temp_last[8 - cpp_offset],
                        m_pres_last[2 - cpp_offset], m_pres_last[3 - cpp_offset], m_pres_last[8 - cpp_offset], m_pres_last[9 - cpp_offset],
                        ref sub_error_code_6, ref UA_LT_calc, ref min_DT_LT);

                    if (sub_error_code_6 > 0)
                    {
                        if (sub_error_code_6 == 11)     // second - law violation in hxr, therefore temp(9) is too low
                        {
                            T9_lower_bound = m_temp_last[9 - cpp_offset];
                            m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;      // bisect bounds for next guess
                            continue;       // cycle T9_loop
                        }
                        else
                        {
                            return;
                        }
                    }

                    // Check for convergence and adjust T9 appropriately.
                    double UA_LT_residual = UA_LT - UA_LT_calc;
                    if (Math.Abs(UA_LT_residual) < 1E-12)
                        break;      // 'exit T9_loop' catches no LT case

                    double secant_guess1 = m_temp_last[9 - cpp_offset] - UA_LT_residual * (last_T9_guess - m_temp_last[9 - cpp_offset]) / (last_LT_residual - UA_LT_residual);   // next guess predicted using secant method

                    if (UA_LT_residual < 0.0)           // UA_LT_calc is too big, temp(9) needs to be higher
                    {
                        if (Math.Abs(UA_LT_residual) / UA_LT < m_tol)
                            break;  // 'exit T9_loop' UA_LT converged (residual is negative)
                        T9_lower_bound = m_temp_last[9 - cpp_offset];
                    }
                    else            // UA_LT_calc is too small, temp(9) needs to be lower
                    {
                        if (UA_LT_residual / UA_LT < m_tol)
                            break; // 'exit T9_loop' UA_LT converged
                        T9_upper_bound = m_temp_last[9 - cpp_offset];
                    }
                    last_LT_residual = UA_LT_residual;              // reset last stored residual value
                    last_T9_guess = m_temp_last[9 - cpp_offset];            // reset last stored guess value

                    // Check if the secant method overshoots and fall back to bisection if it does.
                    if (secant_guess1 <= T9_lower_bound || secant_guess1 >= T9_upper_bound || secant_guess1 != secant_guess1)
                        m_temp_last[9 - cpp_offset] = (T9_lower_bound + T9_upper_bound) * 0.5;
                    else
                        m_temp_last[9 - cpp_offset] = secant_guess1;

                }       // End iteration T9

                // Check that T9_loop converged.
                if (T9_iter >= max_iter)
                {
                    return;
                }

                // State 3 can now be fully defined.
                m_enth_last[3 - cpp_offset] = m_enth_last[2 - cpp_offset] + Q_dot_LT / m_dot_mc;        // energy balance on cold stream of low-temp recuperator

                //property_error_code = CO2_PH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset]);
                wmm = working_fluid.MolecularWeight;
                working_fluid.FindStatueWithPH(m_pres_last[3 - cpp_offset], m_enth_last[3 - cpp_offset] * wmm);

                if (property_error_code != 0)
                {
                    return;
                }

                m_temp_last[3 - cpp_offset] = working_fluid.Temperature;
                m_entr_last[3 - cpp_offset] = working_fluid.Entropy;
                m_dens_last[3 - cpp_offset] = working_fluid.Density;

                // Go through mixing valve
                if (m_recomp_frac >= 1E-12)
                {
                    m_enth_last[4 - cpp_offset] = (1.0 - m_recomp_frac) * m_enth_last[3 - cpp_offset] + m_recomp_frac * m_enth_last[10 - cpp_offset];       // conservation of energy (both sides divided by m_dot_t

                    //property_error_code = CO2_PH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset]);
                    wmm = working_fluid.MolecularWeight;
                    working_fluid.FindStatueWithPH(m_pres_last[4 - cpp_offset], m_enth_last[4 - cpp_offset] * wmm);

                    if (property_error_code != 0)
                    {
                        return;
                    }
                    m_temp_last[4 - cpp_offset] = working_fluid.Temperature;
                    m_entr_last[4 - cpp_offset] = working_fluid.Entropy;
                    m_dens_last[4 - cpp_offset] = working_fluid.Density;
                }
                else        // no mixing value, therefore (4) is equal to (3)
                {
                    m_temp_last[4 - cpp_offset] = m_temp_last[3 - cpp_offset];
                    m_enth_last[4 - cpp_offset] = m_enth_last[3 - cpp_offset];
                    m_entr_last[4 - cpp_offset] = m_entr_last[3 - cpp_offset];
                    m_dens_last[4 - cpp_offset] = m_dens_last[3 - cpp_offset];
                }

                // Check for a second law violation at the outlet of the high-temp recuperator.
                if (m_temp_last[4 - cpp_offset] >= m_temp_last[8 - cpp_offset])     // temp(8) is not valid; it must be higher than it is
                {
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                    continue;       // cycle T8_loop
                }

                // Calculate the UA value of the high-temperature recuperator.
                if (UA_HT < 1E-12)       // no high-temp recuperator (this check is necessary to prevent pressure drops with UA=0 from causing problems)
                    Q_dot_HT = 0.0;
                else
                    Q_dot_HT = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]);

                int sub_error_code_7 = 0;
                calculate_hxr_UA_nuevo(m_N_sub_hxrs, Q_dot_HT, m_dot_t, m_dot_t, m_temp_last[4 - cpp_offset], m_temp_last[7 - cpp_offset],
                    m_pres_last[4 - cpp_offset], m_pres_last[5 - cpp_offset], m_pres_last[7 - cpp_offset], m_pres_last[8 - cpp_offset],
                    ref sub_error_code_7, ref UA_HT_calc, ref min_DT_HT);

                if (sub_error_code_7 > 0)
                {
                    if (sub_error_code_7 == 1)      // 2nd law violation in hxr, therefore temp(8) is too low
                    {
                        T8_lower_bound = m_temp_last[8 - cpp_offset];
                        m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for next guess
                        continue;   // cycle T8_loop
                    }
                    else
                    {
                        return;
                    }
                }

                // Check for convergence and adjust T8 appropriately.
                double UA_HT_residual = UA_HT - UA_HT_calc;

                if (Math.Abs(UA_HT_residual) < 1E-12)
                    break;          // exit T8_loop  !catches no HT case

                double secant_guess = m_temp_last[8 - cpp_offset] - UA_HT_residual * (last_T8_guess - m_temp_last[8 - cpp_offset]) / (last_HT_residual - UA_HT_residual);       // next guess predicted using secant method

                if (UA_HT_residual < 0.0)           // UA_HT_calc is too big, temp(8) needs to be higher
                {
                    if (Math.Abs(UA_HT_residual) / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged (residual is negative)
                    T8_lower_bound = m_temp_last[8 - cpp_offset];
                }
                else                                // UA_HT_calc is too small, temp(8) needs to be larger
                {
                    if (UA_HT_residual / UA_HT < m_tol)
                        break;      // exit T8_loop    UA_HT converged
                    T8_upper_bound = m_temp_last[8 - cpp_offset];
                }
                last_HT_residual = UA_HT_residual;          // reset last stored residual value
                last_T8_guess = m_temp_last[8 - cpp_offset];        // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (secant_guess <= T8_lower_bound || secant_guess >= T8_upper_bound)       // secant method overshot, use bisection
                    m_temp_last[8 - cpp_offset] = (T8_lower_bound + T8_upper_bound) * 0.5;
                else
                    m_temp_last[8 - cpp_offset] = secant_guess;

            }       // End iteration on T8

            // Check that T8_loop converged
            if (T8_iter >= max_iter)
            {
                return;
            }

            // State 5 can now be fully defined
            m_enth_last[5 - cpp_offset] = m_enth_last[4 - cpp_offset] + Q_dot_HT / m_dot_t;     // Energy balance on cold stream of high-temp recuperator

            //property_error_code = CO2_PH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset]);
            wmm = working_fluid.MolecularWeight;
            working_fluid.FindStatueWithPH(m_pres_last[5 - cpp_offset], m_enth_last[5 - cpp_offset] * wmm);

            m_temp_last[5 - cpp_offset] = working_fluid.Temperature;
            m_entr_last[5 - cpp_offset] = working_fluid.Entropy;
            m_dens_last[5 - cpp_offset] = working_fluid.Density;

            // Recompression Cycle
            double Q_dot_PHX = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            double Q_dot_RHX = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);
            double m_W_dot_net_last = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_trh * m_dot_t + w_mc1 * m_dot_mc;
            double m_eta_thermal_last = m_W_dot_net_last / (Q_dot_PHX + Q_dot_RHX);

            // Set cycle state point properties.
            cicloRCMCI_withRH.temp = m_temp_last;
            cicloRCMCI_withRH.pres = m_pres_last;
            cicloRCMCI_withRH.enth = m_enth_last;
            cicloRCMCI_withRH.entr = m_entr_last;
            cicloRCMCI_withRH.dens = m_dens_last;

            // Calculate performance metrics for LTR low-temperature recuperator.
            cicloRCMCI_withRH.LT.C_dot_hot = m_dot_t * (m_enth_last[8 - cpp_offset] - m_enth_last[9 - cpp_offset]) / (m_temp_last[8 - cpp_offset] - m_temp_last[9 - cpp_offset]);   // LT recuperator hot stream capacitance rate
            cicloRCMCI_withRH.LT.C_dot_cold = m_dot_mc * (m_enth_last[3 - cpp_offset] - m_enth_last[2 - cpp_offset]) / (m_temp_last[3 - cpp_offset] - m_temp_last[2 - cpp_offset]);  // LT recuperator cold stream capacitance rate
            double C_dot_min_LT = Math.Min(cicloRCMCI_withRH.LT.C_dot_hot, cicloRCMCI_withRH.LT.C_dot_cold);
            double Q_dot_max_LT = C_dot_min_LT * (m_temp_last[8 - cpp_offset] - m_temp_last[2 - cpp_offset]);
            cicloRCMCI_withRH.LT.eff = Q_dot_LT / Q_dot_max_LT;  // definition of effectiveness
            cicloRCMCI_withRH.LT.UA_design = UA_LT_calc;
            cicloRCMCI_withRH.LT.UA = UA_LT_calc;
            cicloRCMCI_withRH.LT.DP_design1 = m_pres_last[2 - cpp_offset] - m_pres_last[3 - cpp_offset];
            cicloRCMCI_withRH.LT.DP_design2 = m_pres_last[8 - cpp_offset] - m_pres_last[9 - cpp_offset];
            cicloRCMCI_withRH.LT.m_dot_design[0] = m_dot_mc;
            cicloRCMCI_withRH.LT.m_dot_design[1] = m_dot_t;
            cicloRCMCI_withRH.LT.T_c_in = m_temp_last[2 - cpp_offset];
            cicloRCMCI_withRH.LT.T_h_in = m_temp_last[8 - cpp_offset];
            cicloRCMCI_withRH.LT.P_c_in = m_pres_last[2 - cpp_offset];
            cicloRCMCI_withRH.LT.P_h_in = m_pres_last[8 - cpp_offset];
            cicloRCMCI_withRH.LT.P_c_out = m_pres_last[3 - cpp_offset];
            cicloRCMCI_withRH.LT.P_h_out = m_pres_last[9 - cpp_offset];
            cicloRCMCI_withRH.LT.Q_dot = Q_dot_LT;
            cicloRCMCI_withRH.LT.min_DT = min_DT_LT;
            cicloRCMCI_withRH.LT.N_sub = m_N_sub_hxrs;

            //Calculate performance metrics for HTR high-temperature recuperator.
            cicloRCMCI_withRH.HT.C_dot_hot = m_dot_t * (m_enth_last[7 - cpp_offset] - m_enth_last[8 - cpp_offset]) / (m_temp_last[7 - cpp_offset] - m_temp_last[8 - cpp_offset]);   // HT recuperator hot stream capacitance rate
            cicloRCMCI_withRH.HT.C_dot_cold = m_dot_t * (m_enth_last[5 - cpp_offset] - m_enth_last[4 - cpp_offset]) / (m_temp_last[5 - cpp_offset] - m_temp_last[4 - cpp_offset]);  // HT recuperator cold stream capacitance rate
            double C_dot_min_HT = Math.Min(cicloRCMCI_withRH.HT.C_dot_hot, cicloRCMCI_withRH.HT.C_dot_cold);
            double Q_dot_max_HT = C_dot_min_HT * (m_temp_last[7 - cpp_offset] - m_temp_last[4 - cpp_offset]);
            cicloRCMCI_withRH.HT.eff = Q_dot_HT / Q_dot_max_HT;  // definition of effectiveness
            cicloRCMCI_withRH.HT.UA_design = UA_HT_calc;
            cicloRCMCI_withRH.HT.UA = UA_HT_calc;
            cicloRCMCI_withRH.HT.DP_design1 = m_pres_last[4 - cpp_offset] - m_pres_last[5 - cpp_offset];
            cicloRCMCI_withRH.HT.DP_design2 = m_pres_last[7 - cpp_offset] - m_pres_last[8 - cpp_offset];
            cicloRCMCI_withRH.HT.m_dot_design[0] = m_dot_t;
            cicloRCMCI_withRH.HT.m_dot_design[1] = m_dot_t;
            cicloRCMCI_withRH.HT.T_c_in = m_temp_last[4 - cpp_offset];
            cicloRCMCI_withRH.HT.T_h_in = m_temp_last[7 - cpp_offset];
            cicloRCMCI_withRH.HT.P_c_in = m_pres_last[4 - cpp_offset];
            cicloRCMCI_withRH.HT.P_h_in = m_pres_last[7 - cpp_offset];
            cicloRCMCI_withRH.HT.P_c_out = m_pres_last[5 - cpp_offset];
            cicloRCMCI_withRH.HT.P_h_out = m_pres_last[8 - cpp_offset];
            cicloRCMCI_withRH.HT.Q_dot = Q_dot_HT;
            cicloRCMCI_withRH.HT.min_DT = min_DT_HT;
            cicloRCMCI_withRH.HT.N_sub = m_N_sub_hxrs;

            // Set relevant values for other heat exchangers (PHX, RHX, PC).
            cicloRCMCI_withRH.PHX.Q_dot = m_dot_t * (m_enth_last[6 - cpp_offset] - m_enth_last[5 - cpp_offset]);
            cicloRCMCI_withRH.PHX.DP_design1 = m_pres_last[5 - cpp_offset] - m_pres_last[6 - cpp_offset];
            cicloRCMCI_withRH.PHX.DP_design2 = 0.0;

            cicloRCMCI_withRH.RHX.Q_dot = m_dot_t * (m_enth_last[12 - cpp_offset] - m_enth_last[11 - cpp_offset]);
            cicloRCMCI_withRH.RHX.DP_design1 = m_pres_last[11 - cpp_offset] - m_pres_last[12 - cpp_offset];
            cicloRCMCI_withRH.RHX.DP_design2 = 0.0;

            cicloRCMCI_withRH.PC.Q_dot = (m_dot_t * (m_enth_last[9 - cpp_offset] - m_enth_last[13 - cpp_offset]));
            cicloRCMCI_withRH.PC.DP_design1 = 0.0;
            cicloRCMCI_withRH.PC.DP_design2 = m_pres_last[9 - cpp_offset] - m_pres_last[13 - cpp_offset];
            //recomp_cycle%PC%m_dot_design = [0.0_dp, m_dot_mc]

            cicloRCMCI_withRH.COOLER.Q_dot = (m_dot_mc * (m_enth_last[14 - cpp_offset] - m_enth_last[1 - cpp_offset]));
            cicloRCMCI_withRH.COOLER.DP_design1 = 0.0;
            cicloRCMCI_withRH.COOLER.DP_design2 = m_pres_last[14 - cpp_offset] - m_pres_last[1 - cpp_offset];

            // Calculate cycle performance metrics.
            cicloRCMCI_withRH.recomp_frac = m_recomp_frac;

            cicloRCMCI_withRH.W_dot_net = w_mc2 * m_dot_mc + w_rc * m_dot_rc + w_t * m_dot_t + w_trh * m_dot_t + w_mc1 * m_dot_mc;

            cicloRCMCI_withRH.eta_thermal = cicloRCMCI_withRH.W_dot_net / (cicloRCMCI_withRH.PHX.Q_dot + cicloRCMCI_withRH.RHX.Q_dot);

            cicloRCMCI_withRH.m_dot_turbine = m_dot_t;
            cicloRCMCI_withRH.conv_tol = m_tol;
        }
      

        // Main Compressor or Recompressor ONE-Stage detail design (Type Sandia Laboratory snl_Compressor.f90 or snl_compressor_tsf.f90)
        public void Main_Compressor_Detail_Design(core luis, Double P1, Double T1, Double P2, Double T2, Double m_dot_turbine,
                                                Double recomp_frac, ref Double D_rotor, ref Double N, ref Double eta,
                                                ref Boolean surge, ref Double phi_min, ref Double phi_max, ref double phi)
        {
            Double snl_phi_design = 0.02971;  // design-point flow coefficient for Sandia compressor (corresponds to max eta)
            Double snl_phi_min = 0.02;        // approximate surge limit for SNL compressor
            Double snl_phi_max = 0.05;        // approximate x-intercept for SNL compressor

            wmm = luis.working_fluid.MolecularWeight;

            // Local Variables
            int error_code = 0;
            Double N_design, eta_design, w_tip_ratio, D_in, h_in, s_in, s_in_mol, T_out, P_out, h_out, dens1, enth1, entr1, enth2, dens2;
            Double D_out, ssnd_out, h_s_out, psi_design, m_dot, w_i, U_tip, N_rad_s;

            luis.working_fluid.FindStateWithTP(T1, P1);
            enth1 = working_fluid.Enthalpy;
            entr1 = working_fluid.Entropy;
            dens1 = working_fluid.Density;

            // Create references to cycle state properties for clarity.
            D_in = dens1;
            h_in = enth1;
            s_in = entr1;

            luis.working_fluid.FindStateWithTP(T2, P2);
            enth2 = working_fluid.Enthalpy;
            dens2 = working_fluid.Density;

            h_out = enth2;
            D_out = dens2;

            //call CO2_TD(T=T_out, D=D_out, error_code=error_code, ssnd=ssnd_out)  ! speed of sound at outlet
            luis.working_fluid.FindStateWithTD(T2, D_out / wmm);
            ssnd_out = luis.working_fluid.speedofsound;

            if (error_code != 0)
            {
                return;
            }

            //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  // outlet specific enthalpy after isentropic compression
            s_in_mol = s_in * wmm;
            luis.working_fluid.FindStatueWithPS(P2, s_in_mol);
            h_s_out = luis.working_fluid.Enthalpy;

            if (error_code != 0)
            {
                return;
            }

            // Calculate psi at the design-point phi using Horner's method 
            psi_design = ((((-498626.0 * snl_phi_design) + 53224.0) * snl_phi_design - 2505.0) * snl_phi_design + 54.6) * snl_phi_design + 0.04049;
            // from dimensionless modified head curve (at design-point, psi and modified psi are equal)

            // Determine required size and speed of compressor.
            m_dot = m_dot_turbine * (1.0 - recomp_frac);  // mass flow rate through compressor (kg/s)
            w_i = h_s_out - h_in;  // positive isentropic specific work of compressor (kJ/kg)
            U_tip = Math.Sqrt(1000.0 * w_i / psi_design);  // rearranging definition of head coefficient and converting kJ to J
            D_rotor = Math.Sqrt(m_dot / (snl_phi_design * D_in * U_tip));  // rearranging definition of flow coefficient
            N_rad_s = U_tip * 2.0 / D_rotor;   // shaft speed in rad/s
            N_design = N_rad_s * 9.549296590;  // shaft speed in rpm

            // Set other compressor variables.
            w_tip_ratio = U_tip / ssnd_out;     // ratio of the tip speed to local (comp outlet) speed of sound
            eta_design = w_i / (h_out - h_in);  // definition of isentropic efficiency
            eta = eta_design;
            phi = snl_phi_design;
            phi_min = snl_phi_min;
            phi_max = snl_phi_max;
            N = N_design;
            surge = false;
        }

        public void ReCompressor_Detail_Design(core luis, Double P1, Double T1, Double P2, Double T2, Double m_dot_turbine,
                                                  Double recomp_frac, ref Double D_rotor, ref Double N, ref Double eta,
                                                  ref Boolean surge, ref Double phi_min, ref Double phi_max, ref double phi)
        {
            Double snl_phi_design = 0.02971;  // design-point flow coefficient for Sandia compressor (corresponds to max eta)
            Double snl_phi_min = 0.02;        // approximate surge limit for SNL compressor
            Double snl_phi_max = 0.05;        // approximate x-intercept for SNL compressor

            wmm = luis.working_fluid.MolecularWeight;

            // Local Variables
            int error_code = 0;
            Double N_design, eta_design, w_tip_ratio, D_in, h_in, s_in, s_in_mol, T_out, P_out, h_out, dens1, enth1, entr1, enth2, dens2;
            Double D_out, ssnd_out, h_s_out, psi_design, m_dot, w_i, U_tip, N_rad_s;

            luis.working_fluid.FindStateWithTP(T1, P1);
            enth1 = working_fluid.Enthalpy;
            entr1 = working_fluid.Entropy;
            dens1 = working_fluid.Density;

            // Create references to cycle state properties for clarity.
            D_in = dens1;
            h_in = enth1;
            s_in = entr1;

            luis.working_fluid.FindStateWithTP(T2, P2);
            enth2 = working_fluid.Enthalpy;
            dens2 = working_fluid.Density;

            h_out = enth2;
            D_out = dens2;

            //call CO2_TD(T=T_out, D=D_out, error_code=error_code, ssnd=ssnd_out)  ! speed of sound at outlet
            luis.working_fluid.FindStateWithTD(T2, D_out / wmm);
            ssnd_out = luis.working_fluid.speedofsound;

            if (error_code != 0)
            {
                return;
            }

            //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  // outlet specific enthalpy after isentropic compression
            s_in_mol = s_in * wmm;
            luis.working_fluid.FindStatueWithPS(P2, s_in_mol);
            h_s_out = luis.working_fluid.Enthalpy;

            if (error_code != 0)
            {
                return;
            }

            // Calculate psi at the design-point phi using Horner's method 
            psi_design = ((((-498626.0 * snl_phi_design) + 53224.0) * snl_phi_design - 2505.0) * snl_phi_design + 54.6) * snl_phi_design + 0.04049;
            // from dimensionless modified head curve (at design-point, psi and modified psi are equal)

            // Determine required size and speed of compressor.
            m_dot = m_dot_turbine * recomp_frac;  // mass flow rate through compressor (kg/s)
            w_i = h_s_out - h_in;  // positive isentropic specific work of compressor (kJ/kg)
            U_tip = Math.Sqrt(1000.0 * w_i / psi_design);  // rearranging definition of head coefficient and converting kJ to J
            D_rotor = Math.Sqrt(m_dot / (snl_phi_design * D_in * U_tip));  // rearranging definition of flow coefficient
            N_rad_s = U_tip * 2.0 / D_rotor;   // shaft speed in rad/s
            N_design = N_rad_s * 9.549296590;  // shaft speed in rpm

            // Set other compressor variables.
            w_tip_ratio = U_tip / ssnd_out;     // ratio of the tip speed to local (comp outlet) speed of sound
            eta_design = w_i / (h_out - h_in);  // definition of isentropic efficiency
            eta = eta_design;
            phi = snl_phi_design;
            phi_min = snl_phi_min;
            phi_max = snl_phi_max;
            N = N_design;
            surge = false;
        }
        
        // ReCompressor TWO-STAGES Design-Point detail design (Type Sandia National Laboratory, Snl_Compressor_tsr)
        public void ReCompressor_TWO_Stages_Detail_Design(core luis, Double P1, Double T1, Double P2, Double T2, Double m_dot_turbine,
                                                  Double recomp_frac, ref Double D_rotor_1, ref Double D_rotor_2, ref Double N1, ref Double eta1,
                                                  ref Boolean surge1, ref Double phi1_min, ref Double phi1_max, ref double phi1)
        {
            // Parameters
            Int64 max_iter = 100;
            Double tolerance = 1.0e-8;  // absolute tolerance for phi and stage efficiency

            Double snl_phi_design = 0.02971;  // design-point flow coefficient for Sandia compressor (corresponds to max eta)
            Double snl_phi_min = 0.02;        // approximate surge limit for SNL compressor
            Double snl_phi_max = 0.05;        // approximate x-intercept for SNL compressor

            wmm = luis.working_fluid.MolecularWeight;

            // Local Variables
            int error_code = 0;
            Double N_design, eta_design, w_tip_ratio, D_in, h_in, s_in, s_in_mol, T_out, P_out, h_out, dens1, enth1, entr1, enth2, dens2, entr1mol;
            Double D_out, ssnd_out, h2_s_out, h1_s_out, h_s_out, psi_design, m_dot, w1_i, w2_i, U_tip_1, U_tip_2, N_rad_s, w, eta_2_req;
            Double P_int, D_int, h_int, s_int, s_int_mol, ssnd_int;
            Double last_residual, last_P_int, lower_bound, upper_bound, eta_stage, ssd1, residual, secant_step, P_secant;

            luis.working_fluid.FindStateWithTP(T1, P1);
            enth1 = working_fluid.Enthalpy;
            entr1 = working_fluid.Entropy;
            dens1 = working_fluid.Density;

            // Create references to cycle state properties for clarity.
            D_in = dens1;
            h_in = enth1;
            s_in = entr1;

            luis.working_fluid.FindStateWithTP(T2, P2);
            enth2 = working_fluid.Enthalpy;
            dens2 = working_fluid.Density;
            h_out = enth2;
            D_out = dens2;

            //call CO2_TD(T=T_out, D=D_out, error_code=error_code, ssnd=ssnd_out)  ! speed of sound at outlet
            luis.working_fluid.FindStateWithTD(T2, D_out / wmm);
            ssnd_out = luis.working_fluid.speedofsound;

            if (error_code != 0)
            {
                return;
            }

            //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  // outlet specific enthalpy after isentropic compression
            s_in_mol = s_in * wmm;
            luis.working_fluid.FindStatueWithPS(P2, s_in_mol);
            h_s_out = luis.working_fluid.Enthalpy;

            if (error_code != 0)
            {
                return;
            }

            // overall isentropic efficiency
            eta_design = (h_s_out - h_in) / (h_out - h_in);
            // mass flow rate through recompressor (kg/s)
            m_dot = m_dot_turbine * recomp_frac;
            // Calculate psi at the design-point phi using Horner's method 
            psi_design = ((((-498626.0 * snl_phi_design) + 53224.0) * snl_phi_design - 2505.0) * snl_phi_design + 54.6) * snl_phi_design + 0.04049;
            // from dimensionless modified head curve (at design-point, psi and modified psi are equal)

            // Prepare intermediate pressure iteration loop.
            last_residual = 0.0;
            last_P_int = 1.0e12;  // ensures bisection will be used for first step
            lower_bound = P1 + 1e-6;
            upper_bound = P2 - 1e-6;
            P_int = (lower_bound + upper_bound) * 0.5;
            eta_stage = eta_design;  // first guess for stage efficiency

            for (int b = 0; b < max_iter; b++)
            {
                // First stage
                //call CO2_PS(P=P_int, S=s_in, error_code=error_code, enth=h_s_out)  ! ideal outlet specific enthalpy after first stage
                entr1mol = entr1 * wmm;
                luis.working_fluid.FindStatueWithPS(P_int, entr1mol);
                h1_s_out = luis.working_fluid.Enthalpy;

                w1_i = h1_s_out - h_in;  // positive isentropic specific work of first stage
                U_tip_1 = Math.Sqrt(1000.0 * w1_i / psi_design);  // rearranging definition of head coefficient and converting kJ to J
                D_rotor_1 = Math.Sqrt(m_dot / (snl_phi_design * D_in * U_tip_1));  // rearranging definition of flow coefficient
                N_rad_s = U_tip_1 * 2.0 / D_rotor_1;   // shaft speed in rad/s
                N_design = N_rad_s * 9.549296590;  // shaft speed in rpm
                N1 = N_design;
                w = w1_i / eta_stage;  // actual first-stage work
                h_int = h_in + w;  // energy balance on first stage

                //call CO2_PH(P=P_int, H=h_int, error_code=error_code, dens=D_int, entr=s_int, ssnd=ssnd_int)
                luis.working_fluid.FindStatueWithPH(P_int, h_int * wmm);
                D_int = luis.working_fluid.Density;
                s_int = luis.working_fluid.Entropy;
                ssnd_int = luis.working_fluid.speedofsound;

                //call CO2_PS(P=P_out, S=s_int, error_code=error_code, enth=h_s_out)  ! ideal outlet specific enthalpy after second stage
                s_int_mol = s_int * wmm;
                luis.working_fluid.FindStatueWithPS(P2, s_int_mol);
                h2_s_out = luis.working_fluid.Enthalpy;

                w2_i = h2_s_out - h_int;  // positive isentropic specific work of second stage
                U_tip_2 = Math.Sqrt(1000.0 * w2_i / psi_design);  // rearranging definition of head coefficient and converting kJ to J
                D_rotor_2 = 2.0 * U_tip_2 / (N_design * 0.104719755);  // required second-stage diameter
                phi1 = m_dot / (D_int * U_tip_2 * D_rotor_2 * D_rotor_2);  // required flow coefficient
                eta_2_req = w2_i / (h_out - h_int);  // required second stage efficiency to achieve overall eta_design

                // Check convergence and update guesses.
                residual = snl_phi_design - phi1;

                if (residual < 0.0)  // P_int guess is too high
                {
                    if ((-residual <= tolerance) && (Math.Abs(eta_stage - eta_2_req) <= tolerance))
                    {
                        return;
                    }
                    upper_bound = P_int;
                }

                else  // P_int guess is too low
                {
                    if ((residual <= tolerance) & (Math.Abs(eta_stage - eta_2_req) <= tolerance))
                    {
                        return;
                    }
                    lower_bound = P_int;
                }

                secant_step = -residual * (last_P_int - P_int) / (last_residual - residual);
                P_secant = P_int + secant_step;
                last_P_int = P_int;
                last_residual = residual;

                if ((P_secant <= lower_bound) || (P_secant >= upper_bound))  // secant method overshot
                {
                    P_int = (lower_bound + upper_bound) * 0.5;
                }
                else if (Math.Abs(secant_step) > Math.Abs((upper_bound - lower_bound) * 0.5))  // take the smaller step to ensure convergence
                {
                    P_int = (lower_bound + upper_bound) * 0.5;
                }
                else
                {
                    P_int = P_secant;  // use secant guess
                }

                eta_stage = 0.5 * (eta_stage + eta_2_req);  // update guess for stage efficienc

                eta1 = eta_stage;
            }

        }

        // SNL Radial Turbine Design-Point detail design (Type Sandia Laboratory SNL_Radial_Turbine)
        public void snl_radial_turbine(core luis, Double P1, Double T1, Double P2, Double T2, Double m_dot_turbine, Double N_design,
                                       ref Double D_turbine, ref Double A_nozzle, ref Double eta, ref Double N,
                                       ref Double nu, ref Double w_tip_ratio)
        {
            Double enth1, entr1, entr1mol, dens1, enth2, entr2, dens2, ssnd1, h_s_out;
            Double w_i, C_s, U_tip, eta_design;

            Double nu_design = 0.7476;  // maximizes efficiency for SNL turbine efficiency curve

            wmm = luis.working_fluid.MolecularWeight;

            luis.working_fluid.FindStateWithTP(T1, P1);
            enth1 = luis.working_fluid.Enthalpy;
            entr1 = luis.working_fluid.Entropy;
            dens1 = luis.working_fluid.Density;

            luis.working_fluid.FindStateWithTP(T2, P2);
            enth2 = luis.working_fluid.Enthalpy;
            entr2 = luis.working_fluid.Entropy;
            dens2 = luis.working_fluid.Density;

            //call CO2_TD(T=T_in, D=D_in, error_code=error_code, ssnd=ssnd_in)  ! speed of sound at inlet
            luis.working_fluid.FindStateWithTD(T1, dens1 / wmm);
            ssnd1 = luis.working_fluid.speedofsound;

            //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! outlet specific enthalpy after isentropic expansion
            entr1mol = entr1 * wmm;
            luis.working_fluid.FindStatueWithPS(P2, entr1mol);
            h_s_out = luis.working_fluid.Enthalpy;

            // Determine necessary turbine parameters.
            nu = nu_design;
            w_i = enth1 - h_s_out;  // isentropic specific work of turbine (kJ/kg)
            C_s = Math.Sqrt(2.0 * w_i * 1000.0);  // spouting velocity in m/s
            U_tip = nu * C_s;  // rearrange definition of nu
            D_turbine = U_tip / (0.5 * N_design * 0.104719755);  // turbine diameter in m
            A_nozzle = (m_dot_turbine / (C_s * dens1));  // turbine effective nozzle area in m2

            // Set other turbine variables.
            w_tip_ratio = U_tip / ssnd1;  // ratio of the tip speed to local (turbine inlet) speed of sound
            eta_design = (enth1 - enth2) / w_i;  // definition of isentropic efficiency
            eta = eta_design;
            N = N_design;
        }

        // Radial Turbine Design-Point detail design (Type Radial_Turbine)
        public void RadialTurbine(core luis, Double P1, Double T1, Double P2, Double T2, Double m_dot_turbine, Double N_design,
                                       ref Double D_turbine, ref Double A_nozzle, ref Double eta, ref Double N, ref Double nu, ref Double w_tip_ratio)
        {
            // Determine the turbine rotor diameter, effective nozzle area, and design-point shaft
            // speed and store values in recomp_cycle%t.
            //
            // Arguments:
            //   recomp_cycle -- a RecompCycle object that defines the simple/recompression cycle at the design point
            //   error_trace -- an ErrorTrace object
            //
            // Notes:
            //   1) The value for recomp_cycle%t%N_design is required to be set.  If it is <= 0.0 then
            //      the value for recomp_cycle%mc%N_design is used (i.e., link the compressor and turbine
            //      shafts).  For this reason, turbine_sizing must be called after compressor_sizing if
            //      the shafts are to be linked.

            Double enth1, entr1, entr1mol, dens1, enth2, entr2, dens2, ssnd1, h_s_out;
            Double w_i, C_s, U_tip, eta_design;

            Double nu_design = 0.707;  // maximizes efficiency for SNL turbine efficiency curve

            wmm = luis.working_fluid.MolecularWeight;

            luis.working_fluid.FindStateWithTP(T1, P1);
            enth1 = luis.working_fluid.Enthalpy;
            entr1 = luis.working_fluid.Entropy;
            dens1 = luis.working_fluid.Density;

            luis.working_fluid.FindStateWithTP(T2, P2);
            enth2 = luis.working_fluid.Enthalpy;
            entr2 = luis.working_fluid.Entropy;
            dens2 = luis.working_fluid.Density;

            //call CO2_TD(T=T_in, D=D_in, error_code=error_code, ssnd=ssnd_in)  ! speed of sound at inlet
            luis.working_fluid.FindStateWithTD(T1, dens1 / wmm);
            ssnd1 = luis.working_fluid.speedofsound;

            //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! outlet specific enthalpy after isentropic expansion
            entr1mol = entr1 * wmm;
            luis.working_fluid.FindStatueWithPS(P2, entr1mol);
            h_s_out = luis.working_fluid.Enthalpy;

            // Determine necessary turbine parameters.
            nu = nu_design;
            w_i = enth1 - h_s_out;  // isentropic specific work of turbine (kJ/kg)
            C_s = Math.Sqrt(2.0 * w_i * 1000.0);  // spouting velocity in m/s
            U_tip = nu * C_s;  // rearrange definition of nu
            D_turbine = U_tip / (0.5 * N_design * 0.104719755);  // turbine diameter in m
            A_nozzle = (m_dot_turbine / (C_s * dens2));  // turbine effective nozzle area in m2

            // Set other turbine variables.
            w_tip_ratio = U_tip / ssnd1;  // ratio of the tip speed to local (turbine inlet) speed of sound
            eta_design = (enth1 - enth2) / w_i;  // definition of isentropic efficiency
            eta = eta_design;
            N = N_design;
        }

        // Radial Turbine Off-Design performance (Type Radial_Turbine)
        public void RadialTurbine_OffDesign(core luis, ref core.Turbine Turbine_Design, Double P1_offdesign, Double T1_offdesign,
                                     Double P2_offdesign, Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                                     ref Double T2_offdesign)
        {
            wmm = luis.working_fluid.MolecularWeight;

            Double enth1_offdesign;
            Double entr1_offdesign;
            Double entr1_offdesign_mol;
            Double dens1_offdesign;
            Double ssnd1_offdesign;

            Double enth2_s_offdesign;
            Double enth2_offdesign;
            Double dens2_offdesign;

            Double C_s;
            Double U_tip;
            Double eta_0;
            Double nu_offdesign;

            //call CO2_TP(T=T_in, P=P_in, error_code=error_code, enth=h_in, entr=s_in, ssnd=ssnd_in)  ! properties at inlet of turbine at Off-Design Conditions
            luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
            enth1_offdesign = luis.working_fluid.Enthalpy;
            entr1_offdesign = luis.working_fluid.Entropy;
            ssnd1_offdesign = luis.working_fluid.speedofsound;
            dens1_offdesign = luis.working_fluid.Density;

            //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! enthalpy at the turbine outlet if the expansion is isentropic
            entr1_offdesign_mol = entr1_offdesign * wmm;
            luis.working_fluid.FindStatueWithPS(P2_offdesign, entr1_offdesign_mol);
            enth2_s_offdesign = luis.working_fluid.Enthalpy;

            // Apply the radial turbine equations for efficiency.
            C_s = Math.Sqrt(2.0 * (enth1_offdesign - enth2_s_offdesign) * 1000.0);  // spouting velocity (m/s)
            U_tip = Turbine_Design.D_rotor * 0.5 * N_offdesign * 0.104719755;  // turbine tip speed (m/s)
            nu_offdesign = U_tip / C_s;  // ratio of tip speed to spouting velocity

            if (Turbine_Design.nu < 1.0)
            {
                eta_0 = 2.0 * nu_offdesign * Math.Sqrt(1.0 - (nu_offdesign * nu_offdesign));  // efficiency from Baines (1.0 at design point)
            }

            else
            {
                eta_0 = 0.0;  // catches nu values just over 1, which leads to sqrt of negative number
            }

            Turbine_Design.eta = eta_0 * Turbine_Design.eta_design;// actual turbine efficiency

            // Calculate the outlet state and allowable mass flow rate.
            enth2_offdesign = enth1_offdesign - Turbine_Design.eta * (enth1_offdesign - enth2_s_offdesign);  // enthalpy at turbine outlet

            //call CO2_PH(P=P_out, H=h_out, error_code=error_code, temp=T_out, dens=D_out)
            luis.working_fluid.FindStatueWithPH(P2_offdesign, enth2_offdesign * wmm);
            T2_offdesign = luis.working_fluid.Temperature;
            dens2_offdesign = luis.working_fluid.Density;

            m_dot_offdesign = C_s * Turbine_Design.A_nozzle * dens2_offdesign;  // mass flow through turbine (kg/s)
            Turbine_Design.w_tip_ratio = U_tip / ssnd1_offdesign;   // ratio of the tip speed to the local (turbine inlet) speed of sound
            Turbine_Design.N = N_offdesign;
        }

        // SNL Turbine Off-Design performance (Type SNL Turbine)
        public void SNL_Turbine_OffDesign(core luis, ref core.Turbine Turbine_Design, Double P1_offdesign, Double T1_offdesign,
                                 Double P2_offdesign, Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                                 ref Double T2_offdesign)
        {
            wmm = luis.working_fluid.MolecularWeight;

            Double enth1_offdesign;
            Double entr1_offdesign;
            Double entr1_offdesign_mol;
            Double dens1_offdesign;
            Double ssnd1_offdesign;

            Double enth2_s_offdesign;
            Double enth2_offdesign;
            Double dens2_offdesign;

            Double C_s;
            Double U_tip;
            Double eta_0;
            Double nu_offdesign;

            //call CO2_TP(T=T_in, P=P_in, error_code=error_code, enth=h_in, entr=s_in, ssnd=ssnd_in)  ! properties at inlet of turbine at Off-Design Conditions
            luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
            enth1_offdesign = luis.working_fluid.Enthalpy;
            entr1_offdesign = luis.working_fluid.Entropy;
            ssnd1_offdesign = luis.working_fluid.speedofsound;
            dens1_offdesign = luis.working_fluid.Density;

            //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! enthalpy at the turbine outlet if the expansion is isentropic
            entr1_offdesign_mol = entr1_offdesign * wmm;
            luis.working_fluid.FindStatueWithPS(P2_offdesign, entr1_offdesign_mol);
            enth2_s_offdesign = luis.working_fluid.Enthalpy;

            // Apply the radial turbine equations for efficiency.
            C_s = Math.Sqrt(2.0 * (enth1_offdesign - enth2_s_offdesign) * 1000.0);  // spouting velocity (m/s)
            U_tip = Turbine_Design.D_rotor * 0.5 * N_offdesign * 0.104719755;  // turbine tip speed (m/s)
            nu_offdesign = U_tip / C_s;  // ratio of tip speed to spouting velocity

            //eta_0 = 0.179921180_dp + 1.3567_dp*turb%nu + 1.3668_dp*turb%nu**2 - 3.0874_dp*turb%nu**3 + 1.0626_dp*turb%nu**4
            eta_0 = (((1.0626 * Turbine_Design.nu - 3.0874) * Turbine_Design.nu + 1.3668) * Turbine_Design.nu + 1.3567) * Turbine_Design.nu + 0.179921180;
            eta_0 = Math.Max(eta_0, 0.0);
            eta_0 = Math.Min(eta_0, 1.0);
            Turbine_Design.eta = eta_0 * Turbine_Design.eta_design;  // actual turbine efficiency

            // Calculate the outlet state and allowable mass flow rate.
            enth2_offdesign = enth1_offdesign - Turbine_Design.eta * (enth1_offdesign - enth2_s_offdesign);  // enthalpy at turbine outlet

            //call CO2_PH(P=P_out, H=h_out, error_code=error_code, temp=T_out, dens=D_out)
            luis.working_fluid.FindStatueWithPH(P2_offdesign, enth2_offdesign * wmm);
            T2_offdesign = luis.working_fluid.Temperature;
            dens2_offdesign = luis.working_fluid.Density;

            m_dot_offdesign = C_s * Turbine_Design.A_nozzle * dens1_offdesign;  // mass flow through turbine (kg/s)
            Turbine_Design.w_tip_ratio = U_tip / ssnd1_offdesign;   // ratio of the tip speed to the local (turbine inlet) speed of sound
            Turbine_Design.N = N_offdesign;
        }

        // Main Compressor Off-Design performance (Type snl_compressor.f90)
        public void SNL_Compressor_OffDesign(core luis, ref core.Compressor One_Stage_Compressor_Design, Double P1_offdesign, Double T1_offdesign,
                                  Double P2_offdesign, Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                                  ref Double T2_offdesign)
        {
            wmm = luis.working_fluid.MolecularWeight;

            Double enth1_offdesign;
            Double entr1_offdesign;
            Double dens1_offdesign;

            Double enth2_s_offdesign;
            Double enth2_offdesign;
            Double ssdn2_offdesign;

            Double U_tip;
            Double eta_0;
            Double phi;
            Double phi_star;
            Double psi_star;
            Double eta_star;
            Double psi;
            Double dh_s;
            Double dh;

            //call CO2_TP(T=T_in, P=P_in, error_code=error_code, enth=h_in, entr=s_in, ssnd=ssnd_in)  ! properties at inlet of turbine at Off-Design Conditions
            luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
            enth1_offdesign = luis.working_fluid.Enthalpy;
            entr1_offdesign = luis.working_fluid.Entropy;
            dens1_offdesign = luis.working_fluid.Density;

            // Calculate the modified flow and head coefficients and efficiency for the SNL compressor.
            U_tip = One_Stage_Compressor_Design.D_rotor * 0.5 * N_offdesign * 0.104719755;  // tip speed in m/s, Dyreby Thesis page 17 equation 3.2
            phi = m_dot_offdesign / (dens1_offdesign * U_tip * Math.Pow(One_Stage_Compressor_Design.D_rotor, 2));    // flow coefficient, Dyreby Thesis page 17 equation 3.1

            if (phi < One_Stage_Compressor_Design.phi_min) // the compressor is operating in the surge region
            {
                One_Stage_Compressor_Design.surge = true;
                phi = One_Stage_Compressor_Design.phi_min;  // reset phi to to its minimum value; this sets psi and eta to be fixed at the values at the surge limit
            }
            else
            {
                One_Stage_Compressor_Design.surge = false;
            }

            phi_star = phi * Math.Pow((N_offdesign / One_Stage_Compressor_Design.N_design), 0.2);  // modified flow coefficient, page 21 Thesis Dyreby, equation (3.12)
            psi_star = ((((-498626.0 * phi_star) + 53224.0) * phi_star - 2505.0) * phi_star + 54.6) * phi_star + 0.04049;  // from dimensionless modified head curve, page 22, Fig.3.4 Thesis Dyreby
            eta_star = ((((-1.638e6 * phi_star) + 182725.0) * phi_star - 8089.0) * phi_star + 168.6) * phi_star - 0.7069;  // from dimensionless modified efficiency curve, page 22, Fig.3.4 Thesis Dyreby
            psi = psi_star / Math.Pow((One_Stage_Compressor_Design.N_design / N_offdesign), Math.Pow((20.0 * phi_star), 3)); // modified head coefficient, page 21 Thesis Dyreby, equation (3.13)
            eta_0 = (eta_star * 1.47528) / (Math.Pow((One_Stage_Compressor_Design.N_design / N_offdesign), Math.Pow((20.0 * phi_star), 5)));  // efficiency is normalized so it equals 1.0 at snl_phi_design, page 21 Thesis Dyreby, equation (3.14)
            One_Stage_Compressor_Design.eta = Math.Max((eta_0 * One_Stage_Compressor_Design.eta_design), 0.0);  // the actual compressor efficiency, not allowed to go negative

            // Calculate the compressor outlet state.
            dh_s = psi * Math.Pow(U_tip, 2) * 0.001;  // ideal enthalpy rise in compressor, from definition of head coefficient (kJ/kg), page 17 Thesis Dyreby, equation (3.3)
            dh = dh_s / One_Stage_Compressor_Design.eta;            // actual enthalpy rise in compressor
            enth2_s_offdesign = enth1_offdesign + dh_s;           // ideal enthalpy at compressor outlet
            enth2_offdesign = enth1_offdesign + dh;               // actual enthalpy at compressor outlet

            //call CO2_HS(H=h_s_out, S=s_in, error_code=error_code, pres=P_out)  ! get the compressor outlet pressure
            luis.working_fluid.FindStatueWithHS(enth2_s_offdesign * wmm, entr1_offdesign * wmm);
            P2_offdesign = luis.working_fluid.Pressure;

            //call CO2_PH(P=P_out, H=h_out, error_code=error_code, temp=T_out, ssnd=ssnd_out)  ! determines compressor outlet temperature and speed of sound
            luis.working_fluid.FindStatueWithPH(P2_offdesign, enth2_offdesign * wmm);
            T2_offdesign = luis.working_fluid.Temperature;
            ssdn2_offdesign = luis.working_fluid.speedofsound;

            // Set a few compressor variables.
            One_Stage_Compressor_Design.phi = phi;
            One_Stage_Compressor_Design.w_tip_ratio = U_tip / ssdn2_offdesign;     // ratio of the tip speed to local (comp outlet) speed of sound
        }

        // ReCompressor Off-Design performance (Type snl_compressor.f90)
        public void SNL_ReCompressor_OffDesign(core luis, ref core.Compressor One_Stage_Compressor_Design, Double P1_offdesign, Double T1_offdesign,
                                  Double P2_offdesign, Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                                  ref Double T2_offdesign)
        {
            wmm = luis.working_fluid.MolecularWeight;

            Double enth1_offdesign;
            Double entr1_offdesign;
            Double dens1_offdesign;
            Double ssdn1_offdesign;

            Double enth2_s_offdesign;
            Double enth2_offdesign;
            Double ssdn2_offdesign;

            Double U_tip;
            Double eta_0;
            Double phi;
            Double phi_star;
            Double psi_star;
            Double eta_star;
            Double psi;
            Double dh_s;
            Double dh;
            Double alpha;
            Double dh_s_calc;
            Double residual;
            Boolean first_pass;
            Int64 max_iterations = 1000;
            Double tolerance = 1.0e-33;  // absolute tolerance for phi
            Double last_phi = 0;
            Double next_phi;
            Double last_residual = 0;


            //call CO2_TP(T=T_in, P=P_in, error_code=error_code, enth=h_in, entr=s_in, ssnd=ssnd_in)  ! properties at inlet of turbine at Off-Design Conditions
            luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
            entr1_offdesign = luis.working_fluid.Entropy;
            enth1_offdesign = luis.working_fluid.Enthalpy;
            dens1_offdesign = luis.working_fluid.Density;
            ssdn1_offdesign = luis.working_fluid.speedofsound;

            //call CO2_PS(P=P_out, S=s_in, error_code=error_code, enth=h_s_out)  ! outlet enthalpy if compression/expansion is isentropic
            luis.working_fluid.FindStatueWithPS(P2_offdesign, entr1_offdesign * wmm);
            enth2_s_offdesign = luis.working_fluid.Enthalpy;

            dh_s = enth2_s_offdesign - enth1_offdesign;  // ideal enthalpy rise in compressor

            // Iterate on phi.
            alpha = m_dot_offdesign / (dens1_offdesign * Math.Pow(One_Stage_Compressor_Design.D_rotor, 2));  // used to reduce operation count in loop
            phi = One_Stage_Compressor_Design.phi_design;  // start with design-point value
            first_pass = true;

            for (int iter = 1; iter <= max_iterations; iter++)
            {
                U_tip = alpha / phi;  // flow coefficient rearranged (with alpha substitution)
                N_offdesign = (U_tip * 2.0 / One_Stage_Compressor_Design.D_rotor) * 9.549296590;  // shaft speed in rpm
                phi_star = phi * Math.Pow((N_offdesign / One_Stage_Compressor_Design.N_design), 0.2);  // modified flow coefficient
                psi_star = ((((-498626.0 * phi_star) + 53224.0) * phi_star - 2505.0) * phi_star + 54.6) * phi_star + 0.04049;  // from dimensionless modified head curve
                psi = psi_star / Math.Pow((One_Stage_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 3)));
                dh_s_calc = psi * Math.Pow(U_tip, 2) * 0.001;  // calculated ideal enthalpy rise in compressor, from definition of head coefficient (kJ/kg)
                residual = dh_s - dh_s_calc;

                if (Math.Abs(residual) <= tolerance)  // converged sufficiently
                {
                    MessageBox.Show("Please not introduce the EXACT solution from Design-Point in the Off Design-Point.");
                    return;
                    //goto outer;
                }

                if (first_pass == true)
                {
                    next_phi = phi * 1.0001; // take a small step
                    first_pass = false;
                }

                else
                {
                    next_phi = phi - residual * (last_phi - phi) / (last_residual - residual);  // next guess predicted using secant method
                }

                last_phi = phi;
                last_residual = residual;
                phi = next_phi;

                // Check for convergence.
                if (iter >= max_iterations) // did not converge
                {
                    MessageBox.Show("Please not introduce the EXACT solution from Design-Point in the Off Design-Point.");
                    return;
                }

                // Calculate efficiency and outlet state.
                eta_star = ((((-1.638e6 * phi_star) + 182725.0) * phi_star - 8089.0) * phi_star + 168.6) * phi_star - 0.7069;  // from dimensionless modified efficiency curve
                eta_0 = eta_star * 1.47528 / (Math.Pow((One_Stage_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 5))));  // efficiency is normalized so it equals 1.0 at snl_phi_design
                One_Stage_Compressor_Design.eta = Math.Max(eta_0 * One_Stage_Compressor_Design.eta_design, 0.0);  // the actual compressor efficiency, not allowed to go negative
                dh = dh_s / One_Stage_Compressor_Design.eta;              // actual enthalpy rise in compressor
                enth2_offdesign = enth1_offdesign + dh;                 // actual enthalpy at compressor outlet

                //call CO2_PH(P=P_out, H=h_out, error_code=error_code, temp=T_out, ssnd=ssnd_out)  ! determines compressor outlet temperature and speed of sound
                luis.working_fluid.FindStatueWithPH(P2_offdesign, enth2_offdesign * wmm);
                T2_offdesign = luis.working_fluid.Temperature;
                ssdn2_offdesign = luis.working_fluid.speedofsound;

                One_Stage_Compressor_Design.N = N_offdesign;
                One_Stage_Compressor_Design.phi = phi;
                One_Stage_Compressor_Design.w_tip_ratio = U_tip / ssdn2_offdesign; // ratio of the tip speed to local (comp outlet) speed of sound
            }

            //outer:

            //return;

        }

        // Main Compressor Off-Design performance (Type snl_compressor_tsr.f90)
        public void SNL_ReCompressor_TWO_Stages_OffDesign(core luis, ref core.Compressor TWO_Stages_Compressor_Design, Double P1_offdesign, Double T1_offdesign,
                                  Double P2_offdesign, Double N_offdesign, ref Double error_code, ref Double m_dot_offdesign,
                                  ref Double T2_offdesign)
        {
            wmm = luis.working_fluid.MolecularWeight;

            Int64 max_iter = 100;
            Double rel_tol = 1.0e-9;   // relative tolerance for pressure
            Double phi_1;
            Boolean first_pass;
            Double next_phi;
            Double last_phi_1 = 0;
            Double last_residual = 0;

            Double entr1_offdesign, enth1_offdesign, dens1_offdesign, ssdn1_offdesign, ssdn2_offdesign;
            Double U_tip_1, eta_0, eta_stage_1, P_out_calc;
            Double phi_star, psi_star, psi, eta_star, P_int, D_int, s_int, ssnd_int;
            Double dh_s, dh, h_int, h_s_out, h_out, U_tip_2, phi_2, eta_stage_2, residual;

            //call CO2_TP(T=T_in, P=P_in, error_code=error_code, dens=rho_in, enth=h_in, entr=s_in)  ! fully define the inlet state of the compressor
            luis.working_fluid.FindStateWithTP(T1_offdesign, P1_offdesign);
            entr1_offdesign = luis.working_fluid.Entropy;
            enth1_offdesign = luis.working_fluid.Enthalpy;
            dens1_offdesign = luis.working_fluid.Density;
            ssdn1_offdesign = luis.working_fluid.speedofsound;

            // Iterate on first-stage phi.
            phi_1 = TWO_Stages_Compressor_Design.phi_design;  // start with design-point value
            first_pass = true;

            for (int iter = 1; iter <= max_iter; iter++)
            {
                // First stage - dh_s and eta_stage_1.
                U_tip_1 = m_dot_offdesign / (phi_1 * dens1_offdesign * Math.Pow(TWO_Stages_Compressor_Design.D_rotor, 2));  // flow coefficient rearranged
                N_offdesign = (U_tip_1 * 2.0 / TWO_Stages_Compressor_Design.D_rotor) * 9.549296590;  // shaft speed in rpm
                phi_star = phi_1 * (Math.Pow((N_offdesign / TWO_Stages_Compressor_Design.N_design), 0.2));  // modified flow coefficient
                psi_star = ((((-498626.0 * phi_star) + 53224.0) * phi_star - 2505.0) * phi_star + 54.6) * phi_star + 0.04049;  // from dimensionless modified head curve
                psi = psi_star / (Math.Pow((TWO_Stages_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 3))));
                dh_s = psi * Math.Pow(U_tip_1, 2) * 0.001;  // calculated ideal enthalpy rise in first stage of compressor, from definition of head coefficient (kJ/kg)
                eta_star = ((((-1.638e6 * phi_star) + 182725.0) * phi_star - 8089.0) * phi_star + 168.6) * phi_star - 0.7069;  // from dimensionless modified efficiency curve
                eta_0 = eta_star * 1.47528 / (Math.Pow((TWO_Stages_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 5))));  // stage efficiency is normalized so it equals 1.0 at snl_phi_design
                eta_stage_1 = Math.Max(eta_0 * TWO_Stages_Compressor_Design.eta_design, 0.0);  // the actual stage efficiency, not allowed to go negative

                // Calculate first-stage outlet (second-stage inlet) state.
                dh = dh_s / eta_stage_1;  // actual enthalpy rise in first stage
                h_s_out = enth1_offdesign + dh_s;    // ideal enthalpy between stages
                h_int = enth1_offdesign + dh;          // actual enthalpy between stages

                //call CO2_HS(H=h_s_out, S=s_in, error_code=error_code, pres=P_int)  ! get the first-stage outlet pressure (second-stage inlet pressure)
                luis.working_fluid.FindStatueWithHS(h_s_out * wmm, entr1_offdesign * wmm);
                P_int = luis.working_fluid.Pressure;

                //call CO2_PH(P=P_int, H=h_int, error_code=error_code, dens=D_int, entr=s_int, ssnd=ssnd_int)  ! get second-stage inlet properties
                luis.working_fluid.FindStatueWithPH(P_int, h_int * wmm);
                D_int = luis.working_fluid.Density;
                s_int = luis.working_fluid.Entropy;
                ssnd_int = luis.working_fluid.speedofsound;

                // Second stage - dh_s and eta_stage_2.
                U_tip_2 = TWO_Stages_Compressor_Design.D_rotor_2 * 0.5 * N_offdesign * 0.104719755;  // second-stage tip speed in m/s
                phi_2 = m_dot_offdesign / (D_int * U_tip_2 * Math.Pow(TWO_Stages_Compressor_Design.D_rotor_2, 2));   // second-stage flow coefficient
                phi_star = phi_2 * (Math.Pow((N_offdesign / TWO_Stages_Compressor_Design.N_design), 0.2));  // modified flow coefficient
                psi_star = ((((-498626.0 * phi_star) + 53224.0) * phi_star - 2505.0) * phi_star + 54.6) * phi_star + 0.04049;  //from dimensionless modified head curve
                psi = psi_star / (Math.Pow((TWO_Stages_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 3))));
                dh_s = psi * Math.Pow(U_tip_2, 2) * 0.001;  // calculated ideal enthalpy rise in second stage of compressor, from definition of head coefficient (kJ/kg)
                eta_star = ((((-1.638e6 * phi_star) + 182725.0) * phi_star - 8089.0) * phi_star + 168.6) * phi_star - 0.7069;  // from dimensionless modified efficiency curve
                eta_0 = eta_star * 1.47528 / (Math.Pow((TWO_Stages_Compressor_Design.N_design / N_offdesign), (Math.Pow((20.0 * phi_star), 5))));  // stage efficiency is normalized so it equals 1.0 at snl_phi_design
                eta_stage_2 = Math.Max(eta_0 * TWO_Stages_Compressor_Design.eta_design, 0.0);  // the actual stage efficiency, not allowed to go negative

                // Calculate second-stage outlet state.
                dh = dh_s / eta_stage_2;  // actual enthalpy rise in second stage
                h_s_out = h_int + dh_s;   // ideal enthalpy at compressor outlet
                h_out = h_int + dh;       // actual enthalpy at compressor outlet

                //call CO2_HS(H=h_s_out, S=s_int, error_code=error_code, pres=P_out_calc)  ! get the calculated compressor outlet pressure
                luis.working_fluid.FindStatueWithHS(h_s_out * wmm, s_int * wmm);
                P_out_calc = luis.working_fluid.Pressure;

                // Check for convergence and adjust phi_1 guess.
                residual = P2_offdesign - P_out_calc;
                if (Math.Abs(residual) / P2_offdesign <= rel_tol)
                {
                    return;  // converged sufficiently
                }

                if (first_pass == true)
                {
                    next_phi = phi_1 * 1.0001;  // take a small step
                    first_pass = false;
                }

                else
                {
                    next_phi = phi_1 - residual * (last_phi_1 - phi_1) / (last_residual - residual);  //next guess predicted using secant method
                }

                last_phi_1 = phi_1;
                last_residual = residual;
                phi_1 = next_phi;

                // Check for convergence.
                if (iter >= max_iter)   // did not converge
                {
                    return;
                }

                // Determine outlet temperature and speed of sound.
                //call CO2_PH(P=P_out_calc, H=h_out, error_code=error_code, temp=T_out, ssnd=ssnd_out)
                luis.working_fluid.FindStatueWithPH(P_out_calc, h_out * wmm);
                T2_offdesign = luis.working_fluid.Temperature;
                ssdn2_offdesign = luis.working_fluid.speedofsound;

                //call CO2_PS(P=P_out_calc, S=s_in, error_code=error_code, enth=h_s_out)  ! outlet specific enthalpy after isentropic compression
                luis.working_fluid.FindStatueWithPS(P_out_calc, entr1_offdesign * wmm);
                h_s_out = luis.working_fluid.Enthalpy;

                // Set relevant recompressor variables.
                TWO_Stages_Compressor_Design.N = N_offdesign;
                TWO_Stages_Compressor_Design.eta = (h_s_out - enth1_offdesign) / (h_out - enth1_offdesign);  // use overall isentropic efficiency
                TWO_Stages_Compressor_Design.phi = phi_1;
                TWO_Stages_Compressor_Design.phi_2 = phi_2;
                TWO_Stages_Compressor_Design.w_tip_ratio = Math.Max(U_tip_1 / ssnd_int, U_tip_2 / ssdn2_offdesign);  // store maximum ratio
                TWO_Stages_Compressor_Design.surge = ((phi_1 < TWO_Stages_Compressor_Design.phi_min) || (phi_2 < TWO_Stages_Compressor_Design.phi_min));
            }
        }

        public Double off_Design_Point(core luis, ref core.RecompCycle recomp_cycle, Double T_mc_in, Double T_t_in, Double T_trh_in,
                                         Double P_trh_in, Double P_mc_in, Double recomp_frac, Double N_mc, Double N_t, Double N_sub_hxrs,
                                         Double tol, Double error_code)
        {
            //Parameters
            Boolean surge_allowed = true;
            Boolean supersonic_tip_speed_allowed = true;

            // Local Variables
            Int64 m_dot_iter, T9_iter, T8_iter, index;
            m_dot_iter = 1;
            Double rho_in, C_dot_min, Q_dot_max, m_dot_residual, partial_phi, tip_speed;
            Double m_dot_lower_bound, m_dot_upper_bound, m_dot_mc_guess, m_dot_mc_max;
            Double last_m_dot_guess = 0;
            Double last_m_dot_residual = 0;
            Double m_dot_t_allowed = 0;
            Double T9_lower_bound, T9_upper_bound, T8_lower_bound, T8_upper_bound, last_LT_residual, last_T9_guess;
            Double last_HT_residual, last_T8_guess, secant_guess;
            Double m_dot_t, m_dot_mc, m_dot_rc, UA_LT, UA_HT, w_mc, w_rc, w_t, w_trh;
            Double min_DT_LT, min_DT_HT, UA_LT_calc, UA_HT_calc, Q_dot_LT, Q_dot_HT, UA_HT_residual, UA_LT_residual;
            m_dot_mc = 0;

            Double[] temp = new Double[12];
            Double[] pres = new Double[12];
            Double[] enth = new Double[12];
            Double[] entr = new Double[12];
            Double[] dens = new Double[12];

            Double[] DP_LT = new Double[2];
            Double[] DP_HT = new Double[2];
            Double[] DP_PC = new Double[2];
            Double[] DP_PHX = new Double[2];
            Double[] DP_RHX = new Double[2];

            Double[] m_dots = new Double[2];

            Boolean first_pass = false;

            // Parameters
            Int64 max_iter;
            Double temperature_tolerance;  // temperature differences below this are considered zero

            max_iter = 500;
            temperature_tolerance = 1.0e-6;

            // Initialize a few variables.
            temp[0] = T_mc_in;
            pres[0] = P_mc_in;
            temp[5] = T_t_in;
            temp[11] = T_trh_in;
            pres[11] = P_trh_in;
            recomp_cycle.mc.N = N_mc;
            recomp_cycle.t.N = N_t;
            recomp_cycle.conv_tol = tol;

            // Prepare the mass flow rate iteration loop.
            //call CO2_TP(T=temp(1), P=pres(1), error_code=error_code, dens=rho_in)

            luis.working_fluid.FindStateWithTP(temp[0], pres[0]);
            dens[0] = luis.working_fluid.Density;

            tip_speed = recomp_cycle.mc.D_rotor * 0.5 * N_mc * 0.10471975512;  // main compressor tip speed in m/s
            partial_phi = dens[0] * Math.Pow(recomp_cycle.mc.D_rotor, 2) * tip_speed;           // reduces computation on next two lines
            m_dot_mc_guess = recomp_cycle.mc.phi_design * partial_phi;               // mass flow rate corresponding to design-point phi in main compressor
            m_dot_mc_max = recomp_cycle.mc.phi_max * partial_phi * 1.2;           // largest possible mass flow rate in main compressor (with safety factor)
            m_dot_t = m_dot_mc_guess / (1.0 - recomp_frac);                       // first guess for mass flow rate through turbine
            m_dot_upper_bound = m_dot_mc_max / (1.0 - recomp_frac);               // largest possible mass flow rate through turbine
            m_dot_lower_bound = 0.0;                                             // this lower bound allows for surge (checked after iteration)
            first_pass = true;

            for (int j = 1; j < max_iter; j++)
            {
                m_dot_rc = m_dot_t * recomp_frac;  // mass flow rate through recompressing compressor
                m_dot_mc = m_dot_t - m_dot_rc;     // mass flow rate through compressor

                // Calculate the pressure rise through the main compressor.
                luis.SNL_Compressor_OffDesign(luis, ref recomp_cycle.mc, pres[0], temp[0], pres[1], N_mc, ref error_code,
                                              ref m_dot_mc, ref temp[1]);

                if (error_code == 1)  // m_dot is too high because the given shaft speed is not possible
                {
                    m_dot_upper_bound = m_dot_t;
                    m_dot_t = (m_dot_lower_bound + m_dot_upper_bound) * 0.5;  // use bisection for new mass flow rate guess
                    break;
                }

                else if (error_code == 2)  // m_dot is too low because P_out is (likely) above properties limits
                {
                    m_dot_lower_bound = m_dot_t;
                    m_dot_t = (m_dot_lower_bound + m_dot_upper_bound) * 0.5;  // use bisection for new mass flow rate guess
                    break;
                }

                else if (error_code != 0)  // unexpected error
                {
                    MessageBox.Show("Error en Off-Design function");
                    return 0;
                }

                // Calculate scaled pressure drops through heat exchangers.
                m_dots[0] = m_dot_mc;
                m_dots[1] = m_dot_t;
                DP_LT = luis.hxr_pressure_drops(ref recomp_cycle.LT, m_dots);
                m_dots[0] = m_dot_t;
                m_dots[1] = m_dot_t;
                DP_HT = hxr_pressure_drops(ref recomp_cycle.HT, m_dots);
                m_dots[0] = m_dot_t;
                m_dots[1] = 0;
                DP_PHX = hxr_pressure_drops(ref recomp_cycle.PHX, m_dots);
                m_dots[0] = m_dot_t;
                m_dots[1] = 0;
                DP_RHX = hxr_pressure_drops(ref recomp_cycle.RHX, m_dots);
                m_dots[0] = 0;
                m_dots[1] = m_dot_mc;
                DP_RHX = hxr_pressure_drops(ref recomp_cycle.PC, m_dots);

                // Apply pressure drops to heat exchangers, fully defining the pressures at all states.
                pres[2] = pres[1] - DP_LT[0];   // LT recuperator [cold stream]
                pres[3] = pres[2];              // assume no pressure drop in mixing valve
                pres[9] = pres[2];              // assume no pressure drop in mixing valve
                pres[4] = pres[3] - DP_HT[0];   // HT recuperator [cold stream]
                pres[5] = pres[4] - DP_PHX[0];  // PHX
                pres[10] = pres[11] + DP_RHX[0]; //RHX
                pres[8] = pres[0] + DP_PC[1];   // precooler
                pres[7] = pres[8] + DP_LT[1];   // LT recuperator [hot stream]
                pres[6] = pres[7] + DP_HT[1];   // HT recuperator [hot stream]

                // Calculate the mass flow rate through the Main turbine.
                //call off_design_turbine(       &
                //    turb = recomp_cycle%t,     &
                //    T_in = temp(6),            &
                //    P_in = pres(6),            &
                //    P_out = pres(11),           &
                //    N = N_t,                   &
                //    error_trace = error_trace, &
                //    m_dot = m_dot_t_allowed,   &
                //    T_out = temp(11)            &
                //    )

                luis.SNL_Turbine_OffDesign(luis, ref recomp_cycle.t, pres[5], temp[5], pres[10], N_t, ref error_code,
                                           ref m_dot_t_allowed, ref temp[10]);

                // Determine the mass flow rate residual and prepare the next iteration.
                m_dot_residual = m_dot_t - m_dot_t_allowed;
                secant_guess = m_dot_t - m_dot_residual * (last_m_dot_guess - m_dot_t) / (last_m_dot_residual - m_dot_residual);  // next guess predicted using secant method

                if (m_dot_residual > 0.0)  // pressure rise is too small, so m_dot_t is too big
                {
                    if (m_dot_residual / m_dot_t < tol)
                    {
                        break;  // residual is positive; check for convergence
                    }
                    m_dot_upper_bound = m_dot_t;   // reset upper bound
                }

                else  // pressure rise is too high, so m_dot_t is too small
                {
                    if (-m_dot_residual / m_dot_t < tol)
                    {
                        break; // residual is negative; check for convergence
                    }

                    m_dot_lower_bound = m_dot_t;   // reset lower bound
                }

                last_m_dot_residual = m_dot_residual;                                // reset last stored residual value
                last_m_dot_guess = m_dot_t;                                   // reset last stored guess value

                // Check if the secant method overshoots and fall back to bisection if it does.
                if (first_pass)
                {
                    m_dot_t = (m_dot_upper_bound + m_dot_lower_bound) * 0.5;
                    first_pass = false;
                }
                else if ((secant_guess < m_dot_lower_bound) || (secant_guess > m_dot_upper_bound))  // secant method overshot, use bisection
                {
                    m_dot_t = (m_dot_upper_bound + m_dot_lower_bound) * 0.5;
                }
                else
                {
                    m_dot_t = secant_guess;
                }

                m_dot_iter = m_dot_iter + 1;

            } // End m_dot_loop

            // Check for convergence.
            if (m_dot_iter >= max_iter)
            {
                error_code = 42;
                MessageBox.Show("Error in Off-Design function, above max_iter");
                return 0;
            }

            luis.SNL_Turbine_OffDesign(luis, ref recomp_cycle.t, pres[5], temp[5], pres[10], N_t, ref error_code,
                                          ref m_dot_t_allowed, ref temp[10]);

            luis.SNL_Turbine_OffDesign(luis, ref recomp_cycle.t_rh, pres[11], temp[11], pres[6], N_t, ref error_code,
                                        ref m_dot_t_allowed, ref temp[6]);

            luis.working_fluid.FindStateWithTP(temp[0], pres[0]);
            enth[0] = luis.working_fluid.Enthalpy;
            entr[0] = luis.working_fluid.Entropy;
            dens[0] = luis.working_fluid.Density;

            luis.working_fluid.FindStateWithTP(temp[1], pres[1]);
            enth[1] = luis.working_fluid.Enthalpy;
            entr[1] = luis.working_fluid.Entropy;
            dens[1] = luis.working_fluid.Density;

            luis.working_fluid.FindStateWithTP(temp[5], pres[5]);
            enth[5] = luis.working_fluid.Enthalpy;
            entr[5] = luis.working_fluid.Entropy;
            dens[5] = luis.working_fluid.Density;

            luis.working_fluid.FindStateWithTP(temp[10], pres[10]);
            enth[10] = luis.working_fluid.Enthalpy;
            entr[10] = luis.working_fluid.Entropy;
            dens[10] = luis.working_fluid.Density;

            luis.working_fluid.FindStateWithTP(temp[11], pres[11]);
            enth[11] = luis.working_fluid.Enthalpy;
            entr[11] = luis.working_fluid.Entropy;
            dens[11] = luis.working_fluid.Density;

            luis.working_fluid.FindStateWithTP(temp[6], pres[6]);
            enth[6] = luis.working_fluid.Enthalpy;
            entr[6] = luis.working_fluid.Entropy;
            dens[6] = luis.working_fluid.Density;

            // Get the recuperator conductances corresponding to the converged mass flow rates.
            m_dots[0] = m_dot_mc;
            m_dots[1] = m_dot_t;
            UA_LT = luis.hxr_conductance(ref recomp_cycle.LT, m_dots);
            m_dots[0] = m_dot_t;
            m_dots[1] = m_dot_t;
            UA_HT = luis.hxr_conductance(ref recomp_cycle.HT, m_dots);


            // Outer iteration loop: temp(8), checking against UA_HT.
            if (UA_HT < 1.0e-12)  // no high-temperature recuperator
            {
                T8_lower_bound = temp[6];  // no iteration necessary
                T8_upper_bound = temp[6];  // no iteration necessary
                temp[7] = temp[6];
                UA_HT_calc = 0.0;
                last_HT_residual = 0.0;
                last_T8_guess = temp[6];
            }

            else
            {
                T8_lower_bound = temp[1];   // the absolute lowest temp[8] could be
                T8_upper_bound = temp[6];    // the absolutely highest temp[8] could be
                temp[7] = (T8_lower_bound + T8_upper_bound) * 0.5;  // bisect bounds for first guess
                UA_HT_calc = -1.0;
                last_HT_residual = UA_HT;    // know a priori that with T8 = T7, UA_calc = 0 therefore residual is UA_HT - 0.0
                last_T8_guess = temp[6];
            }


            // T8 and T9 loops 

            return 0;
        }


        // Return an array of the scaled pressure drops (in kPa) for the two streams of the heat exchanger defined by 'hxr'.
        //
        // Inputs:
        //   hxr -- a HeatExchanger type with design-point values set
        //   m_dots -- mass flow rates of the two streams (kg/s) [1: cold, 2: hot]
        //
        public Double[] hxr_pressure_drops(ref core.HeatExchanger HX, Double[] m_dots)
        {
            Double[] hxr_pressure_drops = new Double[2];
            m_dots = new Double[2];

            hxr_pressure_drops[0] = HX.DP_design1 * Math.Pow((m_dots[0] / HX.m_dot_design[0]), 1.75);  //Pressure drop Cold Side
            hxr_pressure_drops[1] = HX.DP_design1 * Math.Pow((m_dots[1] / HX.m_dot_design[1]), 1.75);  //Pressure drop Hot Side

            return hxr_pressure_drops;
        }

        // Return the scaled conductance (in kW/K) of the heat exchanger defined by 'hxr'.
        //
        // Inputs:
        //   hxr -- a HeatExchanger type with design-point values set
        //   m_dots -- mass flow rates of the two streams (kg/s) [1: cold, 2: hot]

        public Double hxr_conductance(ref core.HeatExchanger HX, Double[] m_dots)
        {
            Double m_dot_ratio;
            m_dots = new Double[2];
            Double hxr_conductance_result;

            m_dot_ratio = ((m_dots[0] / HX.m_dot_design[0]) + (m_dots[1] / HX.m_dot_design[1])) * 0.5;  // average the two streams
            hxr_conductance_result = HX.UA_design * Math.Pow(m_dot_ratio, 0.8);

            return hxr_conductance_result;
        }


        //Function for calculating Heat Exchanger Conductance (UA) for supercritical Brayton power cycles
        //Next step will be fixing the Effectiveness in Heat Exchangers
        // , ref Double[] NTU, ref Double[] C_R, ref Double[] eff
        public void calculate_PHX_UA(Double Cp_HTF, Int64 N_sub_hxrs, Double Q_dot, Double m_dot_c, ref Double m_dot_h, Double T_c_in, Double T_h_in, Double P_c_in, Double P_c_out, Double P_h_in,
            Double P_h_out, ref Int64 error_code, ref Double UA, ref Double min_DT, ref Double[] Th1, ref Double[] Tc1, ref Double Effec, ref Double[] Ph1, ref Double[] Pc1, ref Double[] UA_local,
            ref Double NTU_Total, ref Double C_R_Total, ref Double[] NTU, ref Double[] C_R, ref Double[] eff, ref Boolean CR_calculated)
        {

            //Refrigerant working_fluid = new Refrigerant(RefrigerantCategory.NewMixture, "CO2=0.90,METHANE=0.10", ReferenceState.DEF);
            //working_fluid.FindStateWithTP(823.15, 25000);
            //this.textBox3.Text = working_fluid.Density.ToString();
            //this.textBox4.Text = working_fluid.Enthalpy.ToString();
            //this.textBox5.Text = working_fluid.Entropy.ToString();
            //this.textBox11.Text = working_fluid.MolecularWeight.ToString();

            //this.textBox7.Text = working_fluid.CriticalTemperature.ToString();
            //this.textBox6.Text = working_fluid.CriticalPressure.ToString();
            //this.textBox8.Text = working_fluid.CriticalDensity.ToString();

            wmm = working_fluid.MolecularWeight;

            // Calculate the conductance (UA value) and minimum temperature difference of a heat exchanger
            // given its mass flow rates, inlet temperatures, and a rate of heat transfer.
            //
            // Inputs:
            //   N_sub_hxrs -- the number of sub-heat exchangers to use for discretization
            //   Q_dot -- rate of heat transfer in the heat exchanger (kW)
            //   m_dot_c -- cold stream mass flow rate (kg/s)
            //   m_dot_h -- hot stream mass flow rate (kg/s)
            //   T_c_in -- cold stream inlet temperature (K)
            //   T_h_in -- hot stream inlet temperature (K)
            //   P_c_in -- cold stream inlet pressure (kPa)
            //   P_c_out -- cold stream outlet pressure (kPa)
            //   P_h_in -- hot stream inlet pressure (kPa)
            //   P_h_out -- hot stream outlet pressure (kPa)
            //
            // Outputs:
            //   error_trace -- an ErrorTrace object
            //   UA -- heat exchanger conductance (kW/K)
            //   min_DT -- minimum temperature difference ("pinch point") between hot and cold streams in heat exchanger (K)
            //
            // Notes:
            //   1) Total pressure drop for each stream is divided equally among the sub-heat exchangers (i.e., DP is a linear distribution).


            //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
            Double TempH, TempC, h_c_in_mol;
            // Local Variables
            Double h_c_in = 0;
            Double h_h_in = 0;
            Double h_c_out = 0;
            Double h_h_out = 0;
            Double[] P_c = new Double[N_sub_hxrs + 1];
            Double[] P_h = new Double[N_sub_hxrs + 1];
            Double[] T_c = new Double[N_sub_hxrs + 1];
            Double[] T_h = new Double[N_sub_hxrs + 1];
            Double[] h_c = new Double[N_sub_hxrs + 1];
            Double[] h_h = new Double[N_sub_hxrs + 1];
            Double[] tempdifferences = new Double[N_sub_hxrs + 1];

            Double[] C_dot_c = new Double[N_sub_hxrs];
            Double[] C_dot_h = new Double[N_sub_hxrs];
            Double[] C_dot_min = new Double[N_sub_hxrs];
            Double[] C_dot_max = new Double[N_sub_hxrs];

            C_R = new Double[N_sub_hxrs];
            eff = new Double[N_sub_hxrs];
            NTU = new Double[N_sub_hxrs];

            Double contador = 0;

            begining:

            // Check inputs.
            if (T_h_in < T_c_in)
            {
                error_code = 5;
                return;
            }

            if (P_h_in < P_h_out)
            {
                error_code = 6;
                return;
            }

            if (P_c_in < P_c_out)
            {
                error_code = 7;
                return;
            }

            if (Math.Abs(Q_dot) <= 1d - 12)  // very low Q_dot; assume it is zero
            {
                UA = 0.0;
                min_DT = T_h_in - T_c_in;
                return;
            }

            // Assume pressure varies linearly through heat exchanger.
            for (int a = 0; a <= N_sub_hxrs; a++)
            {
                P_c[a] = P_c_out + a * (P_c_in - P_c_out) / N_sub_hxrs;
                P_h[a] = P_h_in - a * (P_h_in - P_h_out) / N_sub_hxrs;

                Pc1[a] = P_c[a];
                Ph1[a] = P_h[a];
            }

            // Calculate inlet enthalpies from known state points.

            //if (present(enth)) enth = enth_mol / wmm
            //if (present(entr)) entr = entr_mol / wmm
            //if (present(ssnd)) ssnd = ssnd_RP

            if (contador < 2)
            {
                //call CO2_TP(T=T_c_in, P=P_c(N_sub_hxrs+1), error_code=error_code, enth=h_c_in)
                working_fluid.FindStateWithTP(T_c_in, P_c[N_sub_hxrs]);
                h_c_in = working_fluid.Enthalpy;

                //call CO2_TP(T=T_h_in, P=P_h(1), error_code=error_code, enth=h_h_in)
                //working_fluid.FindStateWithTP(T_h_in, P_h[0]);
                h_h_in = Cp_HTF * T_h_in;

                // Calculate outlet enthalpies from energy balances supporsing 100% Heat transferred
                h_c_out = h_c_in + Q_dot / m_dot_c;
                h_h_out = h_h_in - Q_dot / m_dot_h;

                // Set up the enthalpy vectors and loop through the sub-heat exchangers, calculating temperatures.
                for (int b = 0; b <= N_sub_hxrs; b++)
                {
                    h_c[b] = h_c_out + b * (h_c_in - h_c_out) / N_sub_hxrs;  // create linear vector of cold stream enthalpies, with index 1 at the cold stream outlet
                    h_h[b] = h_h_in - b * (h_h_in - h_h_out) / N_sub_hxrs;   // create linear vector of hot stream enthalpies, with index 1 at the hot stream inlet
                }

                T_h[0] = T_h_in;  //hot stream inlet temperature

                wmm = working_fluid.MolecularWeight;


                //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
                TempC = h_c[0] * wmm;

                //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
                working_fluid.FindStatueWithPH(P_c[0], TempC);
                T_c[0] = working_fluid.Temperature;

                if (T_c[0] >= T_h[0])  // there was a second law violation in this sub-heat exchanger
                {
                    error_code = 11;
                    return;
                }

                //IMPORTANT!!!: When calling call CO2_PH is necessary before converting the Enthalpy units from kJ/Kg to J/mol

                for (int c = 0; c <= N_sub_hxrs; c++)
                {
                    // call CO2_PH(P=P_h(i), H=h_h(i), error_code=error_code, temp=T_h(i))
                    //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
                    //TempH = h_h[c] * wmm;  // convert enthalpy to molar basis
                    //working_fluid.FindStatueWithPH(P_h[c], TempH);
                    T_h[c] = h_h[c] / Cp_HTF;

                    // call CO2_PH(P=P_c(i), H=h_c(i), error_code=error_code, temp=T_c(i))
                    //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
                    TempC = h_c[c] * wmm;  // convert enthalpy to molar basis
                    working_fluid.FindStatueWithPH(P_c[c], TempC);
                    T_c[c] = working_fluid.Temperature;

                    if (T_c[c] >= T_h[c])  // there was a second law violation in this sub-heat exchanger
                    {
                        error_code = 11;
                        return;
                    }
                }
            }

            else if (contador > 2)
            {
                goto continuar;

            }

            //UP TO HERE VALIDATED Temperatures and Enthapies
            // Perform effectiveness-NTU and UA calculations (note: the below are all array operations).
            if (CR_calculated == true)
            {

                for (int d = 0; d < N_sub_hxrs; d++)
                {
                    C_dot_h[d] = m_dot_h * (h_h[d] - h_h[d + 1]) / (T_h[d] - T_h[d + 1]);  // hot stream capacitance rate
                }

                for (int e = 0; e < N_sub_hxrs; e++)
                {
                    C_dot_c[e] = m_dot_c * (h_c[e] - h_c[e + 1]) / (T_c[e] - T_c[e + 1]);  // cold stream capacitance rate
                }

                for (int f = 0; f <= N_sub_hxrs - 1; f++)
                {
                    C_dot_min[f] = Math.Min(C_dot_h[f], C_dot_c[f]);  // minimum capacitance stream
                    C_dot_max[f] = Math.Max(C_dot_h[f], C_dot_c[f]);  // maximum capacitance stream
                    C_R[f] = C_dot_min[f] / C_dot_max[f];
                    eff[f] = Q_dot / ((N_sub_hxrs * C_dot_min[f] * (T_h[f] - T_c[f + 1])));  // effectiveness of each sub-heat exchanger

                    if (C_R[f] == 1)
                    {
                        NTU[f] = eff[f] / (1 - eff[f]);
                    }

                    else
                    {
                        NTU[f] = Math.Log((1 - eff[f] * C_R[f]) / (1 - eff[f])) / (1 - C_R[f]);  // NTU if C_R does not equal 1
                    }
                }
            }

            else if (CR_calculated == false)
            {
                for (int e = 0; e < N_sub_hxrs; e++)
                {
                    C_dot_c[e] = m_dot_c * (h_c[e] - h_c[e + 1]) / (T_c[e] - T_c[e + 1]);  // cold stream capacitance rate
                    C_R[e] = C_R_Total;
                    C_dot_h[e] = C_dot_c[e] / C_R[e];
                    m_dot_h = C_dot_h[e] / ((h_h[e] - h_h[e + 1]) / (T_h[e] - T_h[e + 1]));
                }

                for (int f = 0; f <= N_sub_hxrs - 1; f++)
                {
                    C_dot_min[f] = Math.Min(C_dot_h[f], C_dot_c[f]);  // minimum capacitance stream
                    C_dot_max[f] = Math.Max(C_dot_h[f], C_dot_c[f]);  // maximum capacitance stream
                    C_R[f] = C_dot_min[f] / C_dot_max[f];
                    eff[f] = Q_dot / ((N_sub_hxrs * C_dot_min[f] * (T_h[f] - T_c[f + 1])));  // effectiveness of each sub-heat exchanger

                    if (C_R[f] == 1)
                    {
                        NTU[f] = eff[f] / (1 - eff[f]);
                    }

                    else
                    {
                        NTU[f] = Math.Log((1 - eff[f] * C_R[f]) / (1 - eff[f])) / (1 - C_R[f]);  // NTU if C_R does not equal 1
                    }
                }

                contador = contador + 1;

                goto begining;
            }

            continuar:

            UA = 0;

            for (int g = 0; g <= N_sub_hxrs - 1; g++)
            {
                UA_local[g] = NTU[g] * C_dot_min[g];
                UA = UA + NTU[g] * C_dot_min[g];  // calculate total UA value for the heat exchanger
                NTU_Total = NTU_Total + NTU[g];
                C_R_Total = C_R[g];
            }

            for (int h = 0; h <= N_sub_hxrs; h++)
            {
                tempdifferences[h] = T_h[h] - T_c[h]; // temperatures differences within the heat exchanger
            }

            min_DT = tempdifferences[0];

            for (int i = 0; i <= N_sub_hxrs; i++)
            {
                if (tempdifferences[i] < min_DT)
                {
                    min_DT = tempdifferences[i]; // find the smallest temperature difference within the heat exchanger
                }

                Th1[i] = T_h[i];
                Tc1[i] = T_c[i];
            }

            // Calculate PHX Effectiveness
            Double C_dot_hot, C_dot_cold, C_dot_min1, Q_dot_max;

            C_dot_hot = m_dot_h * (h_h_in - h_h_out) / (T_h[0] - T_h[N_sub_hxrs]);   // PHX recuperator hot stream capacitance rate
            C_dot_cold = m_dot_c * (h_c_out - h_c_in) / (T_c[0] - T_c[N_sub_hxrs]);  // PXH recuperator cold stream capacitance rate
            C_dot_min1 = Math.Min(C_dot_hot, C_dot_cold);
            Q_dot_max = C_dot_min1 * (T_h[0] - T_c[N_sub_hxrs]);
            Effec = Q_dot / Q_dot_max;  // Definition of effectiveness
        }

        //Function for calculating Heat Exchanger Conductance (UA) for supercritical Brayton power cycles
        //Next step will be fixing the Effectiveness in Heat Exchangers
        public void calculate_Precooler_UA(Double Cp_HTF, Int64 N_sub_hxrs, Double Q_dot, ref Double m_dot_c, Double m_dot_h, Double T_c_in, Double T_h_in, Double P_c_in, Double P_c_out, Double P_h_in,
                Double P_h_out, ref Int64 error_code, ref Double UA, ref Double min_DT, ref Double[] Th1, ref Double[] Tc1, ref Double Effec, ref Double[] Ph1, ref Double[] Pc1, ref Double[] UA_local,
            ref Double NTU_Total, ref Double C_R_Total, ref Boolean CR_calculated)
        {

            wmm = working_fluid.MolecularWeight;

            // Calculate the conductance (UA value) and minimum temperature difference of a heat exchanger
            // given its mass flow rates, inlet temperatures, and a rate of heat transfer.
            //
            // Inputs:
            //   N_sub_hxrs -- the number of sub-heat exchangers to use for discretization
            //   Q_dot -- rate of heat transfer in the heat exchanger (kW)
            //   m_dot_c -- cold stream mass flow rate (kg/s)
            //   m_dot_h -- hot stream mass flow rate (kg/s)
            //   T_c_in -- cold stream inlet temperature (K)
            //   T_h_in -- hot stream inlet temperature (K)
            //   P_c_in -- cold stream inlet pressure (kPa)
            //   P_c_out -- cold stream outlet pressure (kPa)
            //   P_h_in -- hot stream inlet pressure (kPa)
            //   P_h_out -- hot stream outlet pressure (kPa)
            //
            // Outputs:
            //   error_trace -- an ErrorTrace object
            //   UA -- heat exchanger conductance (kW/K)
            //   min_DT -- minimum temperature difference ("pinch point") between hot and cold streams in heat exchanger (K)
            //
            // Notes:
            //   1) Total pressure drop for each stream is divided equally among the sub-heat exchangers (i.e., DP is a linear distribution).


            //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
            Double TempH, TempC, h_c_in_mol;
            // Local Variables
            Double h_c_in = 0;
            Double h_h_in = 0;
            Double h_c_out = 0;
            Double h_h_out = 0;
            Double[] P_c = new Double[N_sub_hxrs + 1];
            Double[] P_h = new Double[N_sub_hxrs + 1];
            Double[] T_c = new Double[N_sub_hxrs + 1];
            Double[] T_h = new Double[N_sub_hxrs + 1];
            Double[] h_c = new Double[N_sub_hxrs + 1];
            Double[] h_h = new Double[N_sub_hxrs + 1];
            Double[] tempdifferences = new Double[N_sub_hxrs + 1];

            Double[] C_dot_c = new Double[N_sub_hxrs];
            Double[] C_dot_h = new Double[N_sub_hxrs];
            Double[] C_dot_min = new Double[N_sub_hxrs];
            Double[] C_dot_max = new Double[N_sub_hxrs];
            Double[] C_R = new Double[N_sub_hxrs];
            Double[] eff = new Double[N_sub_hxrs];
            Double[] NTU = new Double[N_sub_hxrs];

            Double contador = 0;

            begining:

            // Check inputs.
            if (T_h_in < T_c_in)
            {
                error_code = 5;
                return;
            }

            if (P_h_in < P_h_out)
            {
                error_code = 6;
                return;
            }

            if (P_c_in < P_c_out)
            {
                error_code = 7;
                return;
            }

            if (Math.Abs(Q_dot) <= 1d - 12)  // very low Q_dot; assume it is zero
            {
                UA = 0.0;
                min_DT = T_h_in - T_c_in;
                return;
            }

            // Assume pressure varies linearly through heat exchanger.
            for (int a = 0; a <= N_sub_hxrs; a++)
            {
                P_c[a] = P_c_out + a * (P_c_in - P_c_out) / N_sub_hxrs;
                P_h[a] = P_h_in - a * (P_h_in - P_h_out) / N_sub_hxrs;

                Pc1[a] = P_c[a];
                Ph1[a] = P_h[a];
            }

            // Calculate inlet enthalpies from known state points.

            //if (present(enth)) enth = enth_mol / wmm
            //if (present(entr)) entr = entr_mol / wmm
            //if (present(ssnd)) ssnd = ssnd_RP


            if (contador < 2)
            {
                //call CO2_TP(T=T_c_in, P=P_c(N_sub_hxrs+1), error_code=error_code, enth=h_c_in)
                //working_fluid.FindStateWithTP(T_c_in, P_c[N_sub_hxrs]);
                h_c_in = Cp_HTF * T_c_in;

                //call CO2_TP(T=T_h_in, P=P_h(1), error_code=error_code, enth=h_h_in)
                working_fluid.FindStateWithTP(T_h_in, P_h[0]);
                h_h_in = working_fluid.Enthalpy;

                // Calculate outlet enthalpies from energy balances supporsing 100% Heat transferred
                h_c_out = h_c_in + Q_dot / m_dot_c;
                h_h_out = h_h_in - Q_dot / m_dot_h;

                // Set up the enthalpy vectors and loop through the sub-heat exchangers, calculating temperatures.
                for (int b = 0; b <= N_sub_hxrs; b++)
                {
                    h_c[b] = h_c_out + b * (h_c_in - h_c_out) / N_sub_hxrs;  // create linear vector of cold stream enthalpies, with index 1 at the cold stream outlet
                    h_h[b] = h_h_in - b * (h_h_in - h_h_out) / N_sub_hxrs;   // create linear vector of hot stream enthalpies, with index 1 at the hot stream inlet
                }

                T_h[0] = T_h_in;  //hot stream inlet temperature

                wmm = working_fluid.MolecularWeight;


                //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
                //TempC = h_c[0] * wmm;

                //call CO2_PH(P=P_c(1), H=h_c(1), error_code=error_code, temp=T_c(1))  ! cold stream outlet temperature
                //working_fluid.FindStatueWithPH(P_c[0], TempC);
                T_c[0] = h_c[0] / Cp_HTF;

                if (T_c[0] >= T_h[0])  // there was a second law violation in this sub-heat exchanger
                {
                    error_code = 11;
                    return;
                }

                //IMPORTANT!!!: When calling call CO2_PH is necessary before converting the Enthalpy units from kJ/Kg to J/mol

                for (int c = 0; c <= N_sub_hxrs; c++)
                {
                    // call CO2_PH(P=P_h(i), H=h_h(i), error_code=error_code, temp=T_h(i))
                    //IMPORTANT!!!: When calling call CO2_PH is necessary to conver the Enthalpy in J/mol from kJ/Kg
                    TempH = h_h[c] * wmm;  // convert enthalpy to molar basis
                    working_fluid.FindStatueWithPH(P_h[c], TempH);
                    T_h[c] = working_fluid.Temperature;

                    T_c[c] = h_c[c] / Cp_HTF;

                    if (T_c[c] >= T_h[c])  // there was a second law violation in this sub-heat exchanger
                    {
                        error_code = 11;
                        return;
                    }
                }
            }

            else if (contador > 2)
            {
                goto continuar;

            }
            //UP TO HERE VALIDATED Temperatures and Enthapies

            if (CR_calculated == true)
            {
                // Perform effectiveness-NTU and UA calculations (note: the below are all array operations).
                for (int d = 0; d < N_sub_hxrs; d++)
                {
                    C_dot_h[d] = m_dot_h * (h_h[d] - h_h[d + 1]) / (T_h[d] - T_h[d + 1]);  // hot stream capacitance rate
                }

                for (int e = 0; e < N_sub_hxrs; e++)
                {
                    C_dot_c[e] = m_dot_c * (h_c[e] - h_c[e + 1]) / (T_c[e] - T_c[e + 1]);  // cold stream capacitance rate
                }

                for (int f = 0; f <= N_sub_hxrs - 1; f++)
                {
                    C_dot_min[f] = Math.Min(C_dot_h[f], C_dot_c[f]);  // minimum capacitance stream
                    C_dot_max[f] = Math.Max(C_dot_h[f], C_dot_c[f]);  // maximum capacitance stream
                    C_R[f] = C_dot_min[f] / C_dot_max[f];
                    eff[f] = Q_dot / ((N_sub_hxrs * C_dot_min[f] * (T_h[f] - T_c[f + 1])));  // effectiveness of each sub-heat exchanger

                    if (C_R[f] == 1)
                    {
                        NTU[f] = eff[f] / (1 - eff[f]);
                    }

                    else
                    {
                        NTU[f] = Math.Log((1 - eff[f] * C_R[f]) / (1 - eff[f])) / (1 - C_R[f]);  // NTU if C_R does not equal 1
                    }
                }

            }

            else if (CR_calculated == false)
            {
                for (int e = 0; e < N_sub_hxrs; e++)
                {
                    C_dot_h[e] = m_dot_h * (h_h[e] - h_h[e + 1]) / (T_h[e] - T_h[e + 1]);  // hot stream capacitance rate
                    C_R[e] = C_R_Total;
                    C_dot_c[e] = C_dot_h[e] * C_R[e];
                    m_dot_c = C_dot_c[e] / ((h_c[e] - h_c[e + 1]) / (T_c[e] - T_c[e + 1]));
                }

                for (int f = 0; f <= N_sub_hxrs - 1; f++)
                {
                    C_dot_min[f] = Math.Min(C_dot_h[f], C_dot_c[f]);  // minimum capacitance stream
                    C_dot_max[f] = Math.Max(C_dot_h[f], C_dot_c[f]);  // maximum capacitance stream
                    C_R[f] = C_dot_min[f] / C_dot_max[f];
                    eff[f] = Q_dot / ((N_sub_hxrs * C_dot_min[f] * (T_h[f] - T_c[f + 1])));  // effectiveness of each sub-heat exchanger

                    if (C_R[f] == 1)
                    {
                        NTU[f] = eff[f] / (1 - eff[f]);
                    }

                    else
                    {
                        NTU[f] = Math.Log((1 - eff[f] * C_R[f]) / (1 - eff[f])) / (1 - C_R[f]);  // NTU if C_R does not equal 1
                    }
                }

                contador = contador + 1;

                goto begining;
            }

            continuar:

            UA = 0;

            for (int g = 0; g <= N_sub_hxrs - 1; g++)
            {
                UA_local[g] = NTU[g] * C_dot_min[g];
                UA = UA + NTU[g] * C_dot_min[g];  // calculate total UA value for the heat exchanger
                NTU_Total = NTU_Total + NTU[g];
                C_R_Total = C_R[g];
            }

            for (int h = 0; h <= N_sub_hxrs; h++)
            {
                tempdifferences[h] = T_h[h] - T_c[h]; // temperatures differences within the heat exchanger
            }

            min_DT = tempdifferences[0];

            for (int i = 0; i <= N_sub_hxrs; i++)
            {
                if (tempdifferences[i] < min_DT)
                {
                    min_DT = tempdifferences[i]; // find the smallest temperature difference within the heat exchanger
                }

                Th1[i] = T_h[i];
                Tc1[i] = T_c[i];
            }

            // Calculate PHX Effectiveness
            Double C_dot_hot, C_dot_cold, C_dot_min1, Q_dot_max;

            C_dot_hot = m_dot_h * (h_h_in - h_h_out) / (T_h[0] - T_h[15]);   // PHX recuperator hot stream capacitance rate
            C_dot_cold = m_dot_c * (h_c_out - h_c_in) / (T_c[0] - T_c[15]);  // PXH recuperator cold stream capacitance rate
            C_dot_min1 = Math.Min(C_dot_hot, C_dot_cold);
            Q_dot_max = C_dot_min1 * (T_h[0] - T_c[15]);
            Effec = Q_dot / Q_dot_max;  // Definition of effectiveness
        }

        public void PHX_PCHE_Detail_Design(Double Cp_HTF, long N_sub_hxrs, Double Q_dot, Double m_dot_c, Double m_dot_h, Double T_c_in, Double T_h_in,
                                       Double P_c_in, Double P_c_out, Double P_h_in, Double P_h_out, Double Number_Modules,
                                       Double Channel_Heigh, Double Channel_Width, Double Distance_Between_Channels, Double Number_Channels_Heigh_perblock,
                                       Double Number_Channels_Width_perblock, ref Double UA, ref Double min_DT, ref Int64 error_code,
                                       ref Double Total_Length, ref Double Total_AP_h, ref Double Total_AP_c)
        {
            Double Q_dot_permodule;

            Q_dot_permodule = Q_dot / Number_Modules;

            Double[] T_c = new Double[N_sub_hxrs + 1];
            Double[] T_h = new Double[N_sub_hxrs + 1];

            Double[] P_c = new Double[N_sub_hxrs + 1];
            Double[] P_h = new Double[N_sub_hxrs + 1];

            Double[] UA_local = new Double[N_sub_hxrs];

            Double Effec = 0;
            Double NTU_Total = 0;
            Double CR_Total = 0;
            Double[] C_dot_c = new Double[N_sub_hxrs];
            Double[] C_dot_h = new Double[N_sub_hxrs];
            Double[] C_dot_min = new Double[N_sub_hxrs];
            Double[] C_dot_max = new Double[N_sub_hxrs];
            Double[] C_R = new Double[N_sub_hxrs];
            Double[] eff = new Double[N_sub_hxrs];
            Double[] NTU = new Double[N_sub_hxrs];

            decimal[] T_cd = new decimal[N_sub_hxrs + 1];
            decimal[] T_hd = new decimal[N_sub_hxrs + 1];

            Boolean CR_calculated = true;

            this.calculate_PHX_UA(Cp_HTF, N_sub_hxrs, Q_dot, m_dot_c, ref m_dot_h, T_c_in, T_h_in, P_c_in, P_c_out, P_h_in, P_h_out,
                ref error_code, ref UA, ref min_DT, ref T_h, ref T_c, ref Effec, ref P_h, ref P_c, ref UA_local,
                ref NTU_Total, ref CR_Total, ref NTU, ref C_R, ref eff, ref CR_calculated);

            for (int j = 0; j <= N_sub_hxrs; j++)
            {
                T_cd[j] = Convert.ToDecimal(T_c[j]);
                T_hd[j] = Convert.ToDecimal(T_h[j]);
            }

            //PCHE Detail Design with Gnielinsky HTC correlation
            Double Total_Number_Channels;
            Double Heigh_perblock;
            Double Width_perblock;

            Double Flow_cold, Flow_hot;
            Double[] Velocity_h = new Double[N_sub_hxrs];
            Double[] Velocity_c = new Double[N_sub_hxrs];

            Double Hydraulic_Diameter;

            Double[] Reynold_h = new Double[N_sub_hxrs];
            Double[] Reynold_c = new Double[N_sub_hxrs];

            Double[] Darcy_h = new Double[N_sub_hxrs];
            Double[] Darcy_c = new Double[N_sub_hxrs];

            Double[] Nusselt_h = new Double[N_sub_hxrs];
            Double[] Nusselt_c = new Double[N_sub_hxrs];

            Double[] HTC_h = new Double[N_sub_hxrs];
            Double[] HTC_c = new Double[N_sub_hxrs];

            Double[] Length_local = new Double[N_sub_hxrs];

            Double[] AP_h = new Double[N_sub_hxrs];
            Double[] AP_c = new Double[N_sub_hxrs];

            //1. Average Temperature
            Double[] Tave_h = new Double[N_sub_hxrs];
            Double[] Tave_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Tave_h[contador] = (T_h[contador] + T_h[contador + 1]) / 2;
                Tave_c[contador] = (T_c[contador] + T_c[contador + 1]) / 2;
            }

            //2. Densities
            Double[] Density_h = new Double[N_sub_hxrs];
            Double[] Density_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {

                Density_h[contador] = (-0.636 * (Tave_h[contador] - 273.15)) + 2090;
                working_fluid.FindStateWithTP(Tave_c[contador], P_c[contador]);
                Density_c[contador] = working_fluid.Density;
            }

            //3.Viscosities
            Double[] Viscosity_h = new Double[N_sub_hxrs];
            Double[] Viscosity_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {

                Viscosity_h[contador] = (-0.000000000147388 * Math.Pow((Tave_h[contador] - 273.15), 3)) + (0.000000228024134 * Math.Pow((Tave_h[contador] - 273.15), 2)) - (0.000119957203979 * (Tave_h[contador] - 273.15)) + 0.022707419662049;
                working_fluid.FindStateWithTD(Tave_c[contador], Density_c[contador] / wmm);
                Viscosity_c[contador] = (working_fluid.viscosity) / 1000000;
            }

            //4.Densities/Viscosities
            Double[] Densities_Viscosity_Ratio_h = new Double[N_sub_hxrs];
            Double[] Densities_Viscosity_Ratio_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Densities_Viscosity_Ratio_h[contador] = Density_h[contador] / Viscosity_h[contador];
                Densities_Viscosity_Ratio_c[contador] = Density_c[contador] / Viscosity_c[contador];
            }

            //5.Thermal Conductivity
            Double[] Thermal_Conductivity_h = new Double[N_sub_hxrs];
            Double[] Thermal_Conductivity_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Thermal_Conductivity_h[contador] = (0.0002 * (Tave_h[contador] - 273.15)) + 0.443;
                working_fluid.FindStateWithTD(Tave_c[contador], Density_c[contador] / wmm);
                Thermal_Conductivity_c[contador] = working_fluid.thermalconductivity;
            }

            //6.Specific Heat at constant pressure Cp
            Double[] Cp_h = new Double[N_sub_hxrs];
            Double[] Cp_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Cp_h[contador] = (0.0002 * (Tave_h[contador] - 273.15)) + 1.4431;
                working_fluid.FindStateWithTP(Tave_c[contador], P_c[contador]);
                Cp_c[contador] = working_fluid.Cp;
            }

            //7.Prandtl Number: Prandtl = eta * Cpcalc / tcx / wm / 1000
            Double[] Prandtl_h = new Double[N_sub_hxrs];
            Double[] Prandtl_c = new Double[N_sub_hxrs];

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                working_fluid.FindStateWithTP(Tave_h[contador], P_h[contador]);
                Prandtl_h[contador] = (Viscosity_h[contador] * Cp_h[contador] / Thermal_Conductivity_h[contador] / 1000) * 1000000;
                working_fluid.FindStateWithTP(Tave_c[contador], P_c[contador]);
                Prandtl_c[contador] = (Viscosity_c[contador] * Cp_c[contador] / Thermal_Conductivity_c[contador] / wmm / 1000) * 1000000;
            }

            //Initialize the values for channels

            Total_Number_Channels = Number_Channels_Heigh_perblock * Number_Channels_Width_perblock;
            Heigh_perblock = Number_Channels_Heigh_perblock * (Channel_Heigh + Channel_Width + Distance_Between_Channels * 2);
            Width_perblock = Number_Channels_Heigh_perblock * (Channel_Heigh + Channel_Width + Distance_Between_Channels * 2);

            Double MassFlow_cold_permodule, MassFlow_hot_permodule;
            MassFlow_cold_permodule = m_dot_c / Number_Modules;
            MassFlow_hot_permodule = m_dot_h / Number_Modules;

            Flow_cold = MassFlow_cold_permodule / Total_Number_Channels;
            Flow_hot = MassFlow_hot_permodule / Total_Number_Channels;

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Velocity_c[contador] = Flow_cold / (((3.1416 * Channel_Width * Channel_Width) / 2) * Density_c[contador]);
                Velocity_h[contador] = Flow_hot / (((3.1416 * Channel_Width * Channel_Width) / 2) * Density_h[contador]);
            }

            Hydraulic_Diameter = 4 * (3.1416 * Channel_Width * Channel_Width / 2) / ((Channel_Width + Channel_Width + 3.1416 * Channel_Width));

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Reynold_c[contador] = Densities_Viscosity_Ratio_c[contador] * Velocity_c[contador] * Hydraulic_Diameter;
                Reynold_h[contador] = Densities_Viscosity_Ratio_h[contador] * Velocity_h[contador] * Hydraulic_Diameter;
            }

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Darcy_c[contador] = Math.Pow(((0.79 * Math.Log(Reynold_c[contador])) - 1.64), -2);
                Darcy_h[contador] = Math.Pow(((0.79 * Math.Log(Reynold_h[contador])) - 1.64), -2);
            }

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Nusselt_c[contador] = ((Darcy_c[contador] / 8) * (Reynold_c[contador] - 1000) * Prandtl_c[contador]) / (1 + 12.7 * (Math.Pow((Darcy_c[contador] / 8), 0.5)) * (Math.Pow(Prandtl_c[contador], 0.6666666666666) - 1));
                Nusselt_h[contador] = 0.023 * (Math.Pow(Reynold_h[contador], 0.8)) * (Math.Pow(Prandtl_h[contador], 0.3));
            }

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                HTC_c[contador] = Nusselt_c[contador] * Thermal_Conductivity_c[contador] / Hydraulic_Diameter;
                HTC_h[contador] = Nusselt_h[contador] * Thermal_Conductivity_h[contador] / Hydraulic_Diameter;
            }

            Total_Length = 0;

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                UA_local[contador] = UA_local[contador] / Number_Modules;
            }

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Length_local[contador] = UA_local[contador] * (1 / (HTC_h[contador] / 1000) + 1 / (HTC_c[contador] / 1000)) / (2 * Total_Number_Channels * Channel_Width * 2);
                Total_Length = Total_Length + Length_local[contador];
            }

            Total_AP_c = 0;
            Total_AP_h = 0;

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                AP_c[contador] = (Length_local[contador] * Darcy_c[contador] * Density_c[contador] * Velocity_c[contador] * Velocity_c[contador] / (2 * Hydraulic_Diameter)) / 1000000;
                Total_AP_c = Total_AP_c + AP_c[contador];
                AP_h[contador] = (Length_local[contador] * Darcy_h[contador] * Density_h[contador] * Velocity_h[contador] * Velocity_h[contador] / (2 * Hydraulic_Diameter)) / 1000000;
                Total_AP_h = Total_AP_h + AP_h[contador];
            }

            //Graph Detail-Design Results
            decimal[] HTC_cd = new decimal[N_sub_hxrs];
            decimal[] HTC_hd = new decimal[N_sub_hxrs];

            for (int j = 0; j < N_sub_hxrs; j++)
            {
                HTC_cd[j] = Convert.ToDecimal(HTC_c[j]);
                HTC_hd[j] = Convert.ToDecimal(HTC_h[j]);
            }
        }

        public void Recuperators_PCHE_Detail_Design(long N_sub_hxrs, Double Q_dot, Double m_dot_c, Double m_dot_h, Double T_c_in, Double T_h_in,
                                       Double P_c_in, Double P_c_out, Double P_h_in, Double P_h_out, Double Number_Modules,
                                       Double Channel_Heigh, Double Channel_Width, Double Distance_Between_Channels, Double Number_Channels_Heigh_perblock,
                                       Double Number_Channels_Width_perblock, ref Double UA, ref Double min_DT, ref Int64 error_code,
                                       ref Double Total_Length, ref Double Total_AP_h, ref Double Total_AP_c)
        {
            Double Q_dot_permodule;

            Q_dot_permodule = Q_dot / Number_Modules;

            Double[] T_c = new Double[N_sub_hxrs + 1];
            Double[] T_h = new Double[N_sub_hxrs + 1];

            Double[] P_c = new Double[N_sub_hxrs + 1];
            Double[] P_h = new Double[N_sub_hxrs + 1];

            Double[] UA_local = new Double[N_sub_hxrs];
            Double[] NTU_local = new Double[N_sub_hxrs];
            Double[] C_R_local = new Double[N_sub_hxrs];
            Double[] eff_local = new Double[N_sub_hxrs];

            Double Effec = 0;
            Double NTU = 0;
            Double CR = 0;

            decimal[] T_cd = new decimal[N_sub_hxrs + 1];
            decimal[] T_hd = new decimal[N_sub_hxrs + 1];

            this.calculate_hxr_UA(N_sub_hxrs, Q_dot, m_dot_c, m_dot_h, T_c_in, T_h_in, P_c_in, P_c_out, P_h_in, P_h_out,
                ref error_code, ref UA, ref min_DT, ref T_h, ref T_c, ref Effec, ref P_h, ref P_c, ref UA_local,
                ref NTU, ref CR, ref NTU_local, ref C_R_local, ref eff_local);

            for (int j = 0; j <= N_sub_hxrs; j++)
            {
                T_cd[j] = Convert.ToDecimal(T_c[j]);
                T_hd[j] = Convert.ToDecimal(T_h[j]);
            }

            //PCHE Detail Design with Gnielinsky HTC correlation
            Double Total_Number_Channels;
            Double Heigh_perblock;
            Double Width_perblock;

            Double Flow_cold, Flow_hot;
            Double[] Velocity_h = new Double[N_sub_hxrs];
            Double[] Velocity_c = new Double[N_sub_hxrs];

            Double Hydraulic_Diameter;

            Double[] Reynold_h = new Double[N_sub_hxrs];
            Double[] Reynold_c = new Double[N_sub_hxrs];

            Double[] Darcy_h = new Double[N_sub_hxrs];
            Double[] Darcy_c = new Double[N_sub_hxrs];

            Double[] Nusselt_h = new Double[N_sub_hxrs];
            Double[] Nusselt_c = new Double[N_sub_hxrs];

            Double[] HTC_h = new Double[N_sub_hxrs];
            Double[] HTC_c = new Double[N_sub_hxrs];

            Double[] Length_local = new Double[N_sub_hxrs];

            Double[] AP_h = new Double[N_sub_hxrs];
            Double[] AP_c = new Double[N_sub_hxrs];

            //1. Average Temperature
            Double[] Tave_h = new Double[N_sub_hxrs];
            Double[] Tave_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Tave_h[contador] = (T_h[contador] + T_h[contador + 1]) / 2;
                Tave_c[contador] = (T_c[contador] + T_c[contador + 1]) / 2;
            }

            //2. Densities
            Double[] Density_h = new Double[N_sub_hxrs];
            Double[] Density_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                working_fluid.FindStateWithTP(Tave_h[contador], P_h[contador]);
                Density_h[contador] = working_fluid.Density;
                working_fluid.FindStateWithTP(Tave_c[contador], P_c[contador]);
                Density_c[contador] = working_fluid.Density;
            }

            //3.Viscosities
            Double[] Viscosity_h = new Double[N_sub_hxrs];
            Double[] Viscosity_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                working_fluid.FindStateWithTD(Tave_h[contador], Density_h[contador] / wmm);
                Viscosity_h[contador] = (working_fluid.viscosity) / 1000000;
                working_fluid.FindStateWithTD(Tave_c[contador], Density_c[contador] / wmm);
                Viscosity_c[contador] = (working_fluid.viscosity) / 1000000;
            }

            //4.Densities/Viscosities
            Double[] Densities_Viscosity_Ratio_h = new Double[N_sub_hxrs];
            Double[] Densities_Viscosity_Ratio_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Densities_Viscosity_Ratio_h[contador] = Density_h[contador] / Viscosity_h[contador];
                Densities_Viscosity_Ratio_c[contador] = Density_c[contador] / Viscosity_c[contador];
            }

            //5.Thermal Conductivity
            Double[] Thermal_Conductivity_h = new Double[N_sub_hxrs];
            Double[] Thermal_Conductivity_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                working_fluid.FindStateWithTD(Tave_h[contador], Density_h[contador] / wmm);
                Thermal_Conductivity_h[contador] = working_fluid.thermalconductivity;
                working_fluid.FindStateWithTD(Tave_c[contador], Density_c[contador] / wmm);
                Thermal_Conductivity_c[contador] = working_fluid.thermalconductivity;
            }

            //6.Specific Heat at constant pressure Cp
            Double[] Cp_h = new Double[N_sub_hxrs];
            Double[] Cp_c = new Double[N_sub_hxrs];
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                working_fluid.FindStateWithTP(Tave_h[contador], P_h[contador]);
                Cp_h[contador] = working_fluid.Cp;
                working_fluid.FindStateWithTP(Tave_c[contador], P_c[contador]);
                Cp_c[contador] = working_fluid.Cp;
            }

            //7.Prandtl Number: Prandtl = eta * Cpcalc / tcx / wm / 1000
            Double[] Prandtl_h = new Double[N_sub_hxrs];
            Double[] Prandtl_c = new Double[N_sub_hxrs];

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                working_fluid.FindStateWithTP(Tave_h[contador], P_h[contador]);
                Prandtl_h[contador] = (Viscosity_h[contador] * Cp_h[contador] / Thermal_Conductivity_h[contador] / wmm / 1000) * 1000000;
                working_fluid.FindStateWithTP(Tave_c[contador], P_c[contador]);
                Prandtl_c[contador] = (Viscosity_c[contador] * Cp_c[contador] / Thermal_Conductivity_c[contador] / wmm / 1000) * 1000000;
            }

            //Initialize the values for channels
            Total_Number_Channels = Number_Channels_Heigh_perblock * Number_Channels_Width_perblock;
            Heigh_perblock = Number_Channels_Heigh_perblock * (Channel_Heigh + Channel_Width + Distance_Between_Channels * 2);
            Width_perblock = Number_Channels_Heigh_perblock * (Channel_Heigh + Channel_Width + Distance_Between_Channels * 2);

            Double MassFlow_cold_permodule, MassFlow_hot_permodule;
            MassFlow_cold_permodule = m_dot_c / Number_Modules;
            MassFlow_hot_permodule = m_dot_h / Number_Modules;

            Flow_cold = MassFlow_cold_permodule / Total_Number_Channels;
            Flow_hot = MassFlow_hot_permodule / Total_Number_Channels;

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Velocity_c[contador] = Flow_cold / (((3.1416 * Channel_Width * Channel_Width) / 2) * Density_c[contador]);
                Velocity_h[contador] = Flow_hot / (((3.1416 * Channel_Width * Channel_Width) / 2) * Density_h[contador]);
            }

            Hydraulic_Diameter = 4 * (3.1416 * Channel_Width * Channel_Width / 2) / ((Channel_Width + Channel_Width + 3.1416 * Channel_Width));

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Reynold_c[contador] = Densities_Viscosity_Ratio_c[contador] * Velocity_c[contador] * Hydraulic_Diameter;
                Reynold_h[contador] = Densities_Viscosity_Ratio_h[contador] * Velocity_h[contador] * Hydraulic_Diameter;
            }

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Darcy_c[contador] = Math.Pow(((0.79 * Math.Log(Reynold_c[contador])) - 1.64), -2);
                Darcy_h[contador] = Math.Pow(((0.79 * Math.Log(Reynold_h[contador])) - 1.64), -2);
            }

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Nusselt_c[contador] = ((Darcy_c[contador] / 8) * (Reynold_c[contador] - 1000) * Prandtl_c[contador]) / (1 + 12.7 * (Math.Pow((Darcy_c[contador] / 8), 0.5)) * (Math.Pow(Prandtl_c[contador], 0.6666666666666) - 1));
                Nusselt_h[contador] = ((Darcy_h[contador] / 8) * (Reynold_h[contador] - 1000) * Prandtl_h[contador]) / (1 + 12.7 * (Math.Pow((Darcy_h[contador] / 8), 0.5)) * (Math.Pow(Prandtl_h[contador], 0.6666666666666) - 1));
            }

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                HTC_c[contador] = Nusselt_c[contador] * Thermal_Conductivity_c[contador] / Hydraulic_Diameter;
                HTC_h[contador] = Nusselt_h[contador] * Thermal_Conductivity_h[contador] / Hydraulic_Diameter;
            }

            Total_Length = 0;

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                UA_local[contador] = UA_local[contador] / Number_Modules;
            }

            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                Length_local[contador] = UA_local[contador] * (1 / (HTC_h[contador] / 1000) + 1 / (HTC_c[contador] / 1000)) / (2 * Total_Number_Channels * Channel_Width * 2);
                Total_Length = Total_Length + Length_local[contador];
            }

            Total_AP_c = 0;
            Total_AP_h = 0;
            for (int contador = 0; contador < N_sub_hxrs; contador++)
            {
                AP_c[contador] = (Length_local[contador] * Darcy_c[contador] * Density_c[contador] * Velocity_c[contador] * Velocity_c[contador] / (2 * Hydraulic_Diameter)) / 1000000;
                Total_AP_c = Total_AP_c + AP_c[contador];
                AP_h[contador] = (Length_local[contador] * Darcy_h[contador] * Density_h[contador] * Velocity_h[contador] * Velocity_h[contador] / (2 * Hydraulic_Diameter)) / 1000000;
                Total_AP_h = Total_AP_h + AP_h[contador];
            }

            //Graph Detail-Design Results
            decimal[] HTC_cd = new decimal[N_sub_hxrs];
            decimal[] HTC_hd = new decimal[N_sub_hxrs];

            for (int j = 0; j < N_sub_hxrs; j++)
            {
                HTC_cd[j] = Convert.ToDecimal(HTC_c[j]);
                HTC_hd[j] = Convert.ToDecimal(HTC_h[j]);
            }
        }

        //Función para calcular el Coeficiente de Darcy
        public static double funcion_core(double x, double rugosidad1, double diametro1, double reynold1)
        {
            double a = rugosidad1 / (3.7 * diametro1);
            double b = 2.51 / reynold1;
            return -2 * Math.Log10(a + b / Math.Pow(x, 0.5)) - 1.0 / Math.Pow(x, 0.5);
        }

        public static double derivada_core(double x, double rugosidad2, double diametro2, double reynold2)
        {
            double a = rugosidad2 / (3.7 * diametro2);
            double b = 2.51 / reynold2;
            return (b / (a * Math.Pow(x, 1.5) + b * x)) + (1.0 / (2 * Math.Pow(x, 1.5)));
        }

        //Interpolation Example for IAM values calculation
        //Double value;
        //value = interpMethod(10, 0.9766, 11, 0.9723, 10.5);
        Double interpMethod_core(Double x0, Double y0, Double x1, Double y1, Double x)
        {
            return y0 * (x - x1) / (x0 - x1) + y1 * (x - x0) / (x1 - x0);
        }

        public Double rad_core(Double angolo)
        {
            Double rad1;
            rad1 = angolo * Math.PI / 180;
            return rad1;
        }

        public Double gradi_core(Double angolo1)
        {
            Double gradi1;
            gradi1 = angolo1 * 180 / Math.PI;
            return gradi1;
        }

        public void PTC_Solar_Field_Design(String HTF, Double zone, Double Lon, Double Lat, Double DNI, Double DAY,
                                           Double HOUR, Double NominalOpticalEfficiency, Double CleanlinessFactor,
                                           ref Double EndLossFactor, Double CollectorApertureWidth, Double SolarFieldThermalEnergy,
                                           ref Double NumberRows, Double SolarFieldInletTemperature, Double SolarFieldOutputTemperature,
                                           Double CoefficientA1, Double CoefficientA2, Double NumberOfSegments, Double Desired_Mass_Flux,
                                           Double Focal_distance, Double Diameter_Interior, Double m_dot_h, Double Rugosidad,
                                           ref Double anginc, ref Double azimuth, ref Double angzenit,
                                           ref Double alt_solare, ref Double IAMLongitudinal, ref Double IAMTransversal, ref Double IAMOverall,
                                           ref Double ReflectorApertureArea, ref Double Total_Pressure_Drop)
        {
            Double MerSD, B, Egiorno, Eorario, Tsun, decl, angorario;
            List<Double> angles = new List<Double>();
            List<Double> IAM_longitudinal = new List<Double>();
            List<Double> IAM_transversal = new List<Double>();
            Double CrossArea, Actual_Mass_Flux, FACTOR, SolarFieldTemperatureIncrement;
            Double temp1, temp2, temp3;
            Double AdmisibleError, ERROR;
            Double ReflectorArea;
            Double ReflectorLength;
            Double RowLength;
            Double temp4, temp5, temp6, temp7;
            Double LengthIncrement;
            Double ThermalLossesTotal;

            Double[] Temperature = new Double[10];
            Double[] ThermalLosses = new Double[10];
            Double[] PressureDrop = new Double[10];
            Double[] rho = new Double[10];
            Double[] velocity = new Double[10];
            Double[] Reynold_number = new Double[10];
            Double[] Dynamic_viscosity = new Double[10];
            Double[] Darcy = new Double[10];
            Double[] Density_Viscority = new Double[10];

            Double Lf_ave, Caudal_per_row, Collector_Efficiency;
            Double Energyloss_path, NeatAbsorbed_Field, NetAbsorbed_path;
            Double SolarEnergyAbsorbed_path, SolarImpinging_path;

            //IAM Table Loading
            for (int angles1 = 0; angles1 <= 66; angles1++)
            {
                angles.Add(Convert.ToDouble(angles1));
                IAM_transversal.Add(Convert.ToDouble(1));
            }

            IAM_longitudinal.Add(1);
            IAM_longitudinal.Add(0.9992);
            IAM_longitudinal.Add(0.9982);
            IAM_longitudinal.Add(0.9967);
            IAM_longitudinal.Add(0.995);
            IAM_longitudinal.Add(0.9928);
            IAM_longitudinal.Add(0.9903);
            IAM_longitudinal.Add(0.9874);
            IAM_longitudinal.Add(0.9842);
            IAM_longitudinal.Add(0.9806);
            IAM_longitudinal.Add(0.9766);
            IAM_longitudinal.Add(0.9723);
            IAM_longitudinal.Add(0.9677);
            IAM_longitudinal.Add(0.9627);
            IAM_longitudinal.Add(0.9573);
            IAM_longitudinal.Add(0.9516);
            IAM_longitudinal.Add(0.9455);
            IAM_longitudinal.Add(0.9391);
            IAM_longitudinal.Add(0.9323);
            IAM_longitudinal.Add(0.9252);
            IAM_longitudinal.Add(0.9177);
            IAM_longitudinal.Add(0.9099);
            IAM_longitudinal.Add(0.9017);
            IAM_longitudinal.Add(0.8933);
            IAM_longitudinal.Add(0.8844);
            IAM_longitudinal.Add(0.8753);
            IAM_longitudinal.Add(0.8658);
            IAM_longitudinal.Add(0.8559);
            IAM_longitudinal.Add(0.8458);
            IAM_longitudinal.Add(0.8353);
            IAM_longitudinal.Add(0.8245);
            IAM_longitudinal.Add(0.8134);
            IAM_longitudinal.Add(0.8019);
            IAM_longitudinal.Add(0.7902);
            IAM_longitudinal.Add(0.7781);
            IAM_longitudinal.Add(0.7657);
            IAM_longitudinal.Add(0.753);
            IAM_longitudinal.Add(0.74);
            IAM_longitudinal.Add(0.7267);
            IAM_longitudinal.Add(0.7131);
            IAM_longitudinal.Add(0.6992);
            IAM_longitudinal.Add(0.6851);
            IAM_longitudinal.Add(0.6706);
            IAM_longitudinal.Add(0.6558);
            IAM_longitudinal.Add(0.6408);
            IAM_longitudinal.Add(0.6255);
            IAM_longitudinal.Add(0.6099);
            IAM_longitudinal.Add(0.5941);
            IAM_longitudinal.Add(0.578);
            IAM_longitudinal.Add(0.5616);
            IAM_longitudinal.Add(0.545);
            IAM_longitudinal.Add(0.5281);
            IAM_longitudinal.Add(0.511);
            IAM_longitudinal.Add(0.4936);
            IAM_longitudinal.Add(0.476);
            IAM_longitudinal.Add(0.4581);
            IAM_longitudinal.Add(0.4401);
            IAM_longitudinal.Add(0.4217);
            IAM_longitudinal.Add(0.4032);
            IAM_longitudinal.Add(0.3845);
            IAM_longitudinal.Add(0.3655);
            IAM_longitudinal.Add(0.3463);
            IAM_longitudinal.Add(0.3269);
            IAM_longitudinal.Add(0.3074);
            IAM_longitudinal.Add(0.2876);
            IAM_longitudinal.Add(0.2676);
            IAM_longitudinal.Add(0.2475);
            IAM_longitudinal.Add(0.2271);
            IAM_longitudinal.Add(0.2066);
            IAM_longitudinal.Add(0.1859);
            IAM_longitudinal.Add(0.1651);
            IAM_longitudinal.Add(0.1441);
            IAM_longitudinal.Add(0.1229);
            IAM_longitudinal.Add(0.1016);
            IAM_longitudinal.Add(0.0801);
            IAM_longitudinal.Add(0.0585);
            IAM_longitudinal.Add(0.0368);
            IAM_longitudinal.Add(0.0149);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);
            IAM_longitudinal.Add(0);

            IAM_transversal.Add(0.9768);
            IAM_transversal.Add(0.9365);
            IAM_transversal.Add(0.8959);
            IAM_transversal.Add(0.855);
            IAM_transversal.Add(0.8139);
            IAM_transversal.Add(0.7725);
            IAM_transversal.Add(0.7309);
            IAM_transversal.Add(0.689);
            IAM_transversal.Add(0.647);
            IAM_transversal.Add(0.6048);
            IAM_transversal.Add(0.5623);
            IAM_transversal.Add(0.5197);
            IAM_transversal.Add(0.477);
            IAM_transversal.Add(0.4341);
            IAM_transversal.Add(0.391);
            IAM_transversal.Add(0.3479);
            IAM_transversal.Add(0.3046);
            IAM_transversal.Add(0.2613);
            IAM_transversal.Add(0.2178);
            IAM_transversal.Add(0.1743);
            IAM_transversal.Add(0.1308);
            IAM_transversal.Add(0.0872);
            IAM_transversal.Add(0.0436);
            IAM_transversal.Add(0);

            //Optics Calculations
            MerSD = 15 * (-zone);
            B = (DAY - 1);
            B = (B * 360) / 365;
            Egiorno = 229.18 * (0.000075 + 0.001868 * Math.Cos(rad(B)) - 0.032077 * Math.Sin(rad(B)) - 0.014615 * Math.Cos(rad(2 * B)) - 0.04089 * Math.Sin(rad(2 * B)));
            Eorario = Egiorno;
            Tsun = (HOUR) + (MerSD + Lon) / 15 + Eorario / 60;
            decl = 23.45 * Math.Sin(rad(360 * (284 + DAY) / 365));
            angorario = (Tsun - 12) * 15;
            angzenit = gradi(Math.Acos(Math.Cos(rad(decl)) * Math.Cos(rad(Lat)) * Math.Cos(rad(angorario)) + Math.Sin(rad(decl)) * Math.Sin(rad(Lat))));
            anginc = gradi(Math.Acos(Math.Pow(Math.Pow(Math.Cos(rad(angzenit)), 2) + Math.Pow(Math.Cos(rad(decl)), 2) * (Math.Pow(Math.Sin(rad(angorario)), 2)), 0.5)));
            alt_solare = 90 - angzenit;
            azimuth = 180 - (gradi(Math.Asin(-Math.Cos(rad(decl)) * Math.Sin(rad(angorario)) / Math.Cos(rad(alt_solare)))));

            Double IAM_longitudinal_temp1, IAM_transversal_temp1;
            Double IAM_longitudinal_temp2, IAM_transversal_temp2;
            Double angle1_temp, angle2_temp;

            for (int loop = 0; loop <= 90; loop++)
            {
                if (angles[loop] > anginc)
                {
                    angle1_temp = angles[loop - 1];
                    angle2_temp = angles[loop];

                    IAM_longitudinal_temp1 = IAM_longitudinal[loop - 1];
                    IAM_longitudinal_temp2 = IAM_longitudinal[loop];

                    IAM_transversal_temp1 = IAM_transversal[loop - 1];
                    IAM_transversal_temp2 = IAM_transversal[loop];

                    IAMLongitudinal = interpMethod(angle1_temp, IAM_longitudinal_temp1, angle2_temp, IAM_longitudinal_temp2, anginc);
                    IAMTransversal = interpMethod(angle1_temp, IAM_transversal_temp1, angle2_temp, IAM_transversal_temp2, anginc);

                    break;
                }
            }

            AdmisibleError = 1;

            //Solar Field Design Calculations

            CrossArea = Math.PI * Math.Pow(Diameter_Interior / 1000, 2) / 4;

            Actual_Mass_Flux = (m_dot_h / NumberRows) / CrossArea;

            if (Actual_Mass_Flux < Desired_Mass_Flux)
            {
                do
                {
                    if (Actual_Mass_Flux < Desired_Mass_Flux)
                    {
                        NumberRows = NumberRows - 1;
                    }

                    Actual_Mass_Flux = (m_dot_h / NumberRows) / CrossArea;

                } while ((Desired_Mass_Flux - Actual_Mass_Flux) > 1);
            }

            if (Actual_Mass_Flux > Desired_Mass_Flux)
            {
                do
                {
                    if (Actual_Mass_Flux > Desired_Mass_Flux)
                    {
                        NumberRows = NumberRows + 1;
                    }

                    Actual_Mass_Flux = (m_dot_h / NumberRows) / CrossArea;

                } while ((-Desired_Mass_Flux + Actual_Mass_Flux) > 1);
            }

            double contador_bucle = 0;

            begining:

            IAMOverall = IAMLongitudinal * IAMTransversal;
            FACTOR = NominalOpticalEfficiency * CleanlinessFactor * EndLossFactor * IAMOverall;
            SolarFieldTemperatureIncrement = (SolarFieldOutputTemperature - SolarFieldInletTemperature) / (NumberOfSegments - 1);

            //First Step
            temp1 = DNI * FACTOR;

            //Second Step and Third Step
            temp2 = (SolarFieldThermalEnergy * 1000) / temp1;

            //Fourth Step
            temp3 = (temp2 / CollectorApertureWidth) / NumberRows;

            ReflectorApertureArea = temp2;

            // Loop Begin
            do
            {
                ReflectorArea = ReflectorApertureArea / 0.90223;
                ReflectorLength = ReflectorApertureArea / CollectorApertureWidth;
                RowLength = ReflectorLength / NumberRows;
                temp4 = ReflectorApertureArea * FACTOR;

                for (int counter = 0; counter <= Convert.ToInt64(NumberOfSegments) - 1; counter++)
                {
                    if (counter == 0)
                    {
                        Temperature[0] = SolarFieldInletTemperature;
                    }

                    else
                    {
                        Temperature[counter] = Temperature[counter - 1] + SolarFieldTemperatureIncrement;
                    }

                }

                LengthIncrement = RowLength / (NumberOfSegments - 1);

                for (int counter1 = 0; counter1 <= Convert.ToInt64(NumberOfSegments) - 2; counter1++)
                {
                    ThermalLosses[counter1] = LengthIncrement * (((CoefficientA1 * Temperature[counter1 + 1]) + (CoefficientA2 * Temperature[counter1 + 1] * Temperature[counter1 + 1] * Temperature[counter1 + 1] * Temperature[counter1 + 1])) / 1000);
                }

                //ThermalLosses[0] = LengthIncrement * (((CoefficientA1 * Temperature[1]) + (CoefficientA2 * Temperature[1] * Temperature[1] * Temperature[1] * Temperature[1])) / 1000);
                //ThermalLosses[1] = LengthIncrement * (((CoefficientA1 * Temperature[2]) + (CoefficientA2 * Temperature[2] * Temperature[2] * Temperature[2] * Temperature[2])) / 1000);
                //ThermalLosses[2] = LengthIncrement * (((CoefficientA1 * Temperature[3]) + (CoefficientA2 * Temperature[3] * Temperature[3] * Temperature[3] * Temperature[3])) / 1000);
                //ThermalLosses[3] = LengthIncrement * (((CoefficientA1 * Temperature[4]) + (CoefficientA2 * Temperature[4] * Temperature[4] * Temperature[4] * Temperature[4])) / 1000);
                //ThermalLosses[4] = LengthIncrement * (((CoefficientA1 * Temperature[5]) + (CoefficientA2 * Temperature[5] * Temperature[5] * Temperature[5] * Temperature[5])) / 1000);
                //ThermalLosses[5] = LengthIncrement * (((CoefficientA1 * Temperature[6]) + (CoefficientA2 * Temperature[6] * Temperature[6] * Temperature[6] * Temperature[6])) / 1000);
                //ThermalLosses[6] = LengthIncrement * (((CoefficientA1 * Temperature[7]) + (CoefficientA2 * Temperature[7] * Temperature[7] * Temperature[7] * Temperature[7])) / 1000);
                //ThermalLosses[7] = LengthIncrement * (((CoefficientA1 * Temperature[8]) + (CoefficientA2 * Temperature[8] * Temperature[8] * Temperature[8] * Temperature[8])) / 1000);

                Double temporal_total_thermal = 0;

                for (int counter1 = 0; counter1 <= Convert.ToInt64(NumberOfSegments) - 3; counter1++)
                {
                    temporal_total_thermal = temporal_total_thermal + ThermalLosses[counter1];
                }

                ThermalLossesTotal = temporal_total_thermal;

                temp5 = (temp4 * DNI / NumberRows) / 1000;
                temp6 = temp5 - ThermalLossesTotal;
                temp7 = temp6 * NumberRows;
                ERROR = SolarFieldThermalEnergy - temp7;

                ReflectorApertureArea = ReflectorApertureArea + 1;

            } while (ERROR > AdmisibleError);

            //END-LOSSES FACTOR calculation: a=Focal_distance, w=CollectorApertureWidth/2 
            double end_losses_temp1, end_losses_temp2, end_losses_temp3, end_losses_temp4;
            double end_losses_temp5, end_losses_temp6, end_losses_temp7;

            end_losses_temp1 = (12 * (Focal_distance * Focal_distance)) + ((CollectorApertureWidth / 2) * (CollectorApertureWidth / 2));
            end_losses_temp2 = 12 * ((4 * (Focal_distance * Focal_distance)) + ((CollectorApertureWidth / 2) * (CollectorApertureWidth / 2)));
            end_losses_temp3 = end_losses_temp1 / end_losses_temp2;

            end_losses_temp4 = Math.Pow(((4 * (Focal_distance * Focal_distance)) + ((CollectorApertureWidth / 2) * (CollectorApertureWidth / 2))), 2);
            end_losses_temp5 = Focal_distance * Focal_distance;
            end_losses_temp6 = end_losses_temp4 / end_losses_temp5;
            end_losses_temp7 = Math.Pow(end_losses_temp6, 0.5);

            Lf_ave = end_losses_temp3 * end_losses_temp7;

            EndLossFactor = 1 - (Lf_ave * (Math.Tan(anginc * Math.PI / 180)) / RowLength);

            contador_bucle = contador_bucle + 1;

            if (contador_bucle < 10)
            {
                goto begining;
            }

            else
            {

            }

            //SOLAR FIELD PRESSURE DROP CALCULATIONS
            //Reynolds Number calculation
            for (int j = 0; j < Convert.ToInt64(NumberOfSegments); j++)
            {
                Caudal_per_row = m_dot_h / NumberRows;

                //Density calculation
                if (HTF == "Solar Salt")
                {
                    rho[j] = (-0.636 * (Temperature[j])) + 2090;
                }

                else if (HTF == "Hitec XL")
                {
                    rho[j] = (3e-6 * (Temperature[j]) * (Temperature[j])) - 0.8285 * (Temperature[j]) + 2240.3;
                }

                else if (HTF == "Therminol VP1")
                {
                    rho[j] = (-0.0008 * (Temperature[j]) * (Temperature[j])) - 0.6364 * (Temperature[j]) + 1074;
                }

                else if (HTF == "Syltherm_800")
                {
                    rho[j] = (-0.0007 * (Temperature[j]) * (Temperature[j])) - 0.7166 * (Temperature[j]) + 946.03;
                }

                else if (HTF == "Dowtherm_A")
                {
                    rho[j] = (-0.0008 * (Temperature[j]) * (Temperature[j])) - 0.6314 * (Temperature[j]) + 1068.6;
                }

                else if (HTF == "Therminol_75")
                {
                    rho[j] = (-0.0004 * (Temperature[j]) * (Temperature[j])) - 0.596 * (Temperature[j]) + 1090.4;
                }

                else if (HTF == "Liquid Sodium")
                {
                    rho[j] = 219 + 275.32 * (1 - ((Temperature[j]) / 2503.7)) + 511.58 * Math.Pow((1 - ((Temperature[j]) / 2503.7)), 0.5);
                }

                //velocities v = Q/(rho x A)
                velocity[j] = Caudal_per_row / (rho[j] * CrossArea);

                //Dynamic_Viscosity calculation
                if (HTF == "Solar Salt")
                {
                    Dynamic_viscosity[j] = (-0.000000000147388 * Math.Pow((Temperature[j]), 3)) + (0.000000228024134 * Math.Pow((Temperature[j]), 2)) - (0.000119957203979 * (Temperature[j])) + 0.022707419662049;
                }

                else if (HTF == "Hitec XL")
                {
                    Dynamic_viscosity[j] = 1000000 * Math.Pow((Temperature[j]), -3.315);
                }

                else if (HTF == "Therminol VP1")
                {
                    Dynamic_viscosity[j] = (0.0002 * Math.Pow((Temperature[j]), -1.115)) * rho[j];
                }

                else if (HTF == "Syltherm_800")
                {
                    Dynamic_viscosity[j] = (1.1629 * Math.Pow((Temperature[j]), -1.361));
                }

                else if (HTF == "Dowtherm_A")
                {
                    Dynamic_viscosity[j] = (0.2222 * Math.Pow((Temperature[j]), -1.216));
                }

                else if (HTF == "Therminol_75")
                {
                    Dynamic_viscosity[j] = (24.252 * Math.Pow((Temperature[j]), -1.943));
                }

                else if (HTF == "Liquid Sodium")
                {
                    Dynamic_viscosity[j] = Math.Pow(Math.E, (-6.4406 - 0.3958 * Math.Log(Temperature[j]) + (556.835 / (Temperature[j]))));
                }

                //Density_Viscority
                Density_Viscority[j] = rho[j] / Dynamic_viscosity[j];

                //Reynold_number
                Reynold_number[j] = Density_Viscority[j] * velocity[j] * (Diameter_Interior / 1000);

                double to, e1;

                int i = 0;
                Console.Write("Ingresar el valor estimado de x = ");

                Darcy[j] = 0.001;
                do
                {
                    to = Darcy[j];
                    Darcy[j] = Darcy[j] - funcion_Darcy(Darcy[j], Rugosidad, Diameter_Interior, Reynold_number[j]) / derivada_Darcy(Darcy[j], Rugosidad, Diameter_Interior, Reynold_number[j]);
                    e1 = Math.Abs((Darcy[j] - to) / Darcy[j]);
                    i = i + 1;
                } while (e1 > 0.00000000001 && i < 100);

                PressureDrop[j] = ((Darcy[j] * (rho[j] / 2) * (Math.Pow(velocity[j], 2) / (Diameter_Interior / 1000))) * LengthIncrement) / 100000;
            }

            Total_Pressure_Drop = 0;
            for (int z = 0; z < Convert.ToInt64(NumberOfSegments) - 1; z++)
            {
                Total_Pressure_Drop = Total_Pressure_Drop + PressureDrop[z];
            }

            SolarImpinging_path = ((ReflectorApertureArea * DNI) / NumberRows) / 1000;
            SolarEnergyAbsorbed_path = ((ReflectorApertureArea * FACTOR * DNI) / NumberRows) / 1000;
            Energyloss_path = ThermalLossesTotal;
            NetAbsorbed_path = SolarEnergyAbsorbed_path - Energyloss_path;
            NeatAbsorbed_Field = NetAbsorbed_path * NumberRows;
            Collector_Efficiency = (NetAbsorbed_path / SolarImpinging_path) * 100;
        }

        public void LF_Solar_Field_Design(String HTF, Double zone, Double Lon, Double Lat, Double DNI, Double DAY,
                                          Double HOUR, Double NominalOpticalEfficiency, Double CleanlinessFactor,
                                          ref Double EndLossFactor, Double CollectorApertureWidth, Double SolarFieldThermalEnergy,
                                          ref Double NumberRows, Double SolarFieldInletTemperature, Double SolarFieldOutputTemperature,
                                          Double CoefficientA1, Double CoefficientA2, Double NumberOfSegments, Double Desired_Mass_Flux,
                                          Double Focal_distance, Double Diameter_Interior, Double m_dot_h, Double Rugosidad,
                                          ref Double anginc_long, ref Double anginc_trans, ref Double azimuth, ref Double angzenit,
                                          ref Double alt_solare, ref Double IAMLongitudinal, ref Double IAMTransversal, ref Double IAMOverall,
                                          ref Double ReflectorApertureArea, ref Double Total_Pressure_Drop, String IAM_Table_Name)
        {

            //PrUEBA

            Double MerSD, B, Egiorno, Eorario, Tsun, decl, angorario;

            List<Double> angles = new List<Double>();
            List<Double> IAM_longitudinal = new List<Double>();
            List<Double> IAM_transversal = new List<Double>();
            List<Double[]> IAM_thermoflow25 = new List<Double[]>();

            Double CrossArea, Actual_Mass_Flux, FACTOR, SolarFieldTemperatureIncrement;
            Double temp1, temp2, temp3;
            Double AdmisibleError, ERROR;
            Double ReflectorArea;
            Double ReflectorLength;
            Double RowLength;
            Double temp4, temp5, temp6, temp7;
            Double LengthIncrement;
            Double ThermalLossesTotal;

            Double[] Temperature = new Double[10];
            Double[] ThermalLosses = new Double[10];
            Double[] PressureDrop = new Double[10];
            Double[] rho = new Double[10];
            Double[] velocity = new Double[10];
            Double[] Reynold_number = new Double[10];
            Double[] Dynamic_viscosity = new Double[10];
            Double[] Darcy = new Double[10];
            Double[] Density_Viscosity = new Double[10];
            Double[] Receiver_lengths = new Double[10];

            Double Lf_ave, Caudal_per_row, Collector_Efficiency;
            Double Energyloss_path, NeatAbsorbed_Field, NetAbsorbed_path;
            Double SolarEnergyAbsorbed_path, SolarImpinging_path;

            //IAM Table Loading
            for (int angles1 = 0; angles1 <= 90; angles1++)
            {
                angles.Add(Convert.ToDouble(angles1));
            }

            if (IAM_Table_Name == "Thermoflow 21, Novatec Biosol, Fresnel")
            {
                IAM_longitudinal.Add(1);
                IAM_transversal.Add(1);

                IAM_longitudinal.Add(0.999);
                IAM_transversal.Add(0.994);

                IAM_longitudinal.Add(0.998);
                IAM_transversal.Add(0.983);

                IAM_longitudinal.Add(0.997);
                IAM_transversal.Add(0.973);

                IAM_longitudinal.Add(0.995);
                IAM_transversal.Add(0.971);

                IAM_longitudinal.Add(0.993);
                IAM_transversal.Add(0.971);

                IAM_longitudinal.Add(0.991);
                IAM_transversal.Add(0.977);

                IAM_longitudinal.Add(0.988);
                IAM_transversal.Add(0.988);

                IAM_longitudinal.Add(0.985);
                IAM_transversal.Add(0.996);

                IAM_longitudinal.Add(0.982);
                IAM_transversal.Add(0.992);

                IAM_longitudinal.Add(0.978);
                IAM_transversal.Add(0.98);

                IAM_longitudinal.Add(0.974);
                IAM_transversal.Add(0.97);

                IAM_longitudinal.Add(0.97);
                IAM_transversal.Add(0.967);

                IAM_longitudinal.Add(0.965);
                IAM_transversal.Add(0.965);

                IAM_longitudinal.Add(0.96);
                IAM_transversal.Add(0.97);

                IAM_longitudinal.Add(0.955);
                IAM_transversal.Add(0.981);

                IAM_longitudinal.Add(0.949);
                IAM_transversal.Add(0.986);

                IAM_longitudinal.Add(0.943);
                IAM_transversal.Add(0.979);

                IAM_longitudinal.Add(0.936);
                IAM_transversal.Add(0.965);

                IAM_longitudinal.Add(0.929);
                IAM_transversal.Add(0.958);

                IAM_longitudinal.Add(0.922);
                IAM_transversal.Add(0.956);

                IAM_longitudinal.Add(0.915);
                IAM_transversal.Add(0.955);

                IAM_longitudinal.Add(0.907);
                IAM_transversal.Add(0.963);

                IAM_longitudinal.Add(0.899);
                IAM_transversal.Add(0.97);

                IAM_longitudinal.Add(0.89);
                IAM_transversal.Add(0.967);

                IAM_longitudinal.Add(0.88);
                IAM_transversal.Add(0.952);

                IAM_longitudinal.Add(0.871);
                IAM_transversal.Add(0.945);

                IAM_longitudinal.Add(0.862);
                IAM_transversal.Add(0.943);

                IAM_longitudinal.Add(0.852);
                IAM_transversal.Add(0.941);

                IAM_longitudinal.Add(0.841);
                IAM_transversal.Add(0.951);

                IAM_longitudinal.Add(0.831);
                IAM_transversal.Add(0.951);

                IAM_longitudinal.Add(0.819);
                IAM_transversal.Add(0.942);

                IAM_longitudinal.Add(0.808);
                IAM_transversal.Add(0.932);

                IAM_longitudinal.Add(0.796);
                IAM_transversal.Add(0.928);

                IAM_longitudinal.Add(0.783);
                IAM_transversal.Add(0.925);

                IAM_longitudinal.Add(0.771);
                IAM_transversal.Add(0.932);

                IAM_longitudinal.Add(0.758);
                IAM_transversal.Add(0.93);

                IAM_longitudinal.Add(0.744);
                IAM_transversal.Add(0.919);

                IAM_longitudinal.Add(0.731);
                IAM_transversal.Add(0.914);

                IAM_longitudinal.Add(0.716);
                IAM_transversal.Add(0.91);

                IAM_longitudinal.Add(0.702);
                IAM_transversal.Add(0.912);

                IAM_longitudinal.Add(0.687);
                IAM_transversal.Add(0.911);

                IAM_longitudinal.Add(0.672);
                IAM_transversal.Add(0.904);

                IAM_longitudinal.Add(0.656);
                IAM_transversal.Add(0.899);

                IAM_longitudinal.Add(0.64);
                IAM_transversal.Add(0.895);

                IAM_longitudinal.Add(0.623);
                IAM_transversal.Add(0.893);

                IAM_longitudinal.Add(0.606);
                IAM_transversal.Add(0.884);

                IAM_longitudinal.Add(0.589);
                IAM_transversal.Add(0.874);

                IAM_longitudinal.Add(0.571);
                IAM_transversal.Add(0.864);

                IAM_longitudinal.Add(0.553);
                IAM_transversal.Add(0.864);

                IAM_longitudinal.Add(0.535);
                IAM_transversal.Add(0.863);

                IAM_longitudinal.Add(0.515);
                IAM_transversal.Add(0.85);

                IAM_longitudinal.Add(0.496);
                IAM_transversal.Add(0.837);

                IAM_longitudinal.Add(0.476);
                IAM_transversal.Add(0.822);

                IAM_longitudinal.Add(0.456);
                IAM_transversal.Add(0.807);

                IAM_longitudinal.Add(0.435);
                IAM_transversal.Add(0.792);

                IAM_longitudinal.Add(0.414);
                IAM_transversal.Add(0.776);

                IAM_longitudinal.Add(0.392);
                IAM_transversal.Add(0.759);

                IAM_longitudinal.Add(0.37);
                IAM_transversal.Add(0.741);

                IAM_longitudinal.Add(0.348);
                IAM_transversal.Add(0.724);

                IAM_longitudinal.Add(0.325);
                IAM_transversal.Add(0.705);

                IAM_longitudinal.Add(0.303);
                IAM_transversal.Add(0.685);

                IAM_longitudinal.Add(0.28);
                IAM_transversal.Add(0.666);

                IAM_longitudinal.Add(0.258);
                IAM_transversal.Add(0.645);

                IAM_longitudinal.Add(0.236);
                IAM_transversal.Add(0.624);

                IAM_longitudinal.Add(0.214);
                IAM_transversal.Add(0.602);

                IAM_longitudinal.Add(0.193);
                IAM_transversal.Add(0.58);

                IAM_longitudinal.Add(0.173);
                IAM_transversal.Add(0.557);

                IAM_longitudinal.Add(0.153);
                IAM_transversal.Add(0.533);

                IAM_longitudinal.Add(0.135);
                IAM_transversal.Add(0.51);

                IAM_longitudinal.Add(0.117);
                IAM_transversal.Add(0.486);

                IAM_longitudinal.Add(0.1);
                IAM_transversal.Add(0.461);

                IAM_longitudinal.Add(0.085);
                IAM_transversal.Add(0.437);

                IAM_longitudinal.Add(0.071);
                IAM_transversal.Add(0.412);

                IAM_longitudinal.Add(0.059);
                IAM_transversal.Add(0.387);

                IAM_longitudinal.Add(0.047);
                IAM_transversal.Add(0.362);

                IAM_longitudinal.Add(0.037);
                IAM_transversal.Add(0.338);

                IAM_longitudinal.Add(0.029);
                IAM_transversal.Add(0.312);

                IAM_longitudinal.Add(0.022);
                IAM_transversal.Add(0.287);

                IAM_longitudinal.Add(0.016);
                IAM_transversal.Add(0.262);

                IAM_longitudinal.Add(0.011);
                IAM_transversal.Add(0.237);

                IAM_longitudinal.Add(0.007);
                IAM_transversal.Add(0.212);

                IAM_longitudinal.Add(0.004);
                IAM_transversal.Add(0.186);

                IAM_longitudinal.Add(0.002);
                IAM_transversal.Add(0.16);

                IAM_longitudinal.Add(0);
                IAM_transversal.Add(0.133);

                IAM_longitudinal.Add(0);
                IAM_transversal.Add(0.105);

                IAM_longitudinal.Add(0);
                IAM_transversal.Add(0.076);

                IAM_longitudinal.Add(0);
                IAM_transversal.Add(0.048);

                IAM_longitudinal.Add(0);
                IAM_transversal.Add(0.022);

                IAM_longitudinal.Add(0);
                IAM_transversal.Add(0.004);

                IAM_longitudinal.Add(0);
                IAM_transversal.Add(0);
            }

            else if (IAM_Table_Name == "Thermoflow 25, Novatec - Superheater (Fresnel)")
            {
                IAM_longitudinal.Add(1);
                IAM_transversal.Add(1);

                IAM_longitudinal.Add(0.977);
                IAM_transversal.Add(0.979);

                IAM_longitudinal.Add(0.92);
                IAM_transversal.Add(0.959);

                IAM_longitudinal.Add(0.825);
                IAM_transversal.Add(0.953);

                IAM_longitudinal.Add(0.694);
                IAM_transversal.Add(0.912);

                IAM_longitudinal.Add(0.522);
                IAM_transversal.Add(0.858);

                IAM_longitudinal.Add(0.312);
                IAM_transversal.Add(0.7);

                IAM_longitudinal.Add(0.109);
                IAM_transversal.Add(0.48);

                IAM_longitudinal.Add(0.001);
                IAM_transversal.Add(0.232);

                IAM_longitudinal.Add(0);
                IAM_transversal.Add(0);
            }

            double[] IAM_values_thermoflow25;
            IAM_values_thermoflow25 = new double[] { 0, 1, 1 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "0", "1", "1" };
            // dataGridView6.Rows.Add(IAM_values);

            IAM_values_thermoflow25 = new double[] { 10, 0.977, 0.979 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "10", "0.977", "0.979" };
            // dataGridView6.Rows.Add(IAM_values);

            IAM_values_thermoflow25 = new double[] { 20, 0.92, 0.959 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "20", "0.92", "0.959" };
            // dataGridView6.Rows.Add(IAM_values);

            IAM_values_thermoflow25 = new double[] { 30, 0.825, 0.953 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "30", "0.825", "0.953" };
            // dataGridView6.Rows.Add(IAM_values);

            IAM_values_thermoflow25 = new double[] { 40, 0.694, 0.912 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "40", "0.694", "0.912" };
            // dataGridView6.Rows.Add(IAM_values);

            IAM_values_thermoflow25 = new double[] { 50, 0.522, 0.858 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "50", "0.522", "0.858" };
            // dataGridView6.Rows.Add(IAM_values);

            IAM_values_thermoflow25 = new double[] { 60, 0.312, 0.7 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "60", "0.312", "0.7" };
            // dataGridView6.Rows.Add(IAM_values);

            IAM_values_thermoflow25 = new double[] { 70, 0.109, 0.48 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "70", "0.109", "0.48" };
            // dataGridView6.Rows.Add(IAM_values);

            IAM_values_thermoflow25 = new double[] { 80, 0.001, 0.232 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "80", "0.001", "0.232" };
            // dataGridView6.Rows.Add(IAM_values);

            IAM_values_thermoflow25 = new double[] { 90, 0, 0 };
            IAM_thermoflow25.Add(IAM_values_thermoflow25);

            // IAM_values = new string[] { "90", "0", "0" };
            // dataGridView6.Rows.Add(IAM_values);

            //Optics Calculations
            MerSD = 15 * (-zone);
            B = (DAY - 1);
            B = (B * 360) / 365;
            Egiorno = 229.18 * (0.000075 + 0.001868 * Math.Cos(rad(B)) - 0.032077 * Math.Sin(rad(B)) - 0.014615 * Math.Cos(rad(2 * B)) - 0.04089 * Math.Sin(rad(2 * B)));
            Eorario = Egiorno;
            Tsun = (HOUR) + (MerSD + Lon) / 15 + Eorario / 60;
            decl = 23.45 * Math.Sin(rad(360 * (284 + DAY) / 365));
            angorario = (Tsun - 12) * 15;
            angzenit = gradi(Math.Acos(Math.Cos(rad(decl)) * Math.Cos(rad(Lat)) * Math.Cos(rad(angorario)) + Math.Sin(rad(decl)) * Math.Sin(rad(Lat))));
            //anginc = gradi(Math.Acos(Math.Pow(Math.Pow(Math.Cos(rad(angzenit)), 2) + Math.Pow(Math.Cos(rad(decl)), 2) * (Math.Pow(Math.Sin(rad(angorario)), 2)), 0.5)));

            alt_solare = 90 - angzenit;
            azimuth = 180 - (gradi(Math.Asin(-Math.Cos(rad(decl)) * Math.Sin(rad(angorario)) / Math.Cos(rad(alt_solare)))));

            anginc_long = gradi(Math.Acos(Math.Pow(1 - (Math.Pow(Math.Cos(rad(azimuth)), 2) * Math.Pow(Math.Cos(rad(alt_solare)), 2)), 0.5)));
            anginc_trans = gradi(Math.Atan(Math.Sin(rad(azimuth)) / Math.Tan(rad(alt_solare))));

            Double IAM_longitudinal_temp1 = 0;
            Double IAM_transversal_temp1 = 0;
            Double IAM_longitudinal_temp2 = 0;
            Double IAM_transversal_temp2 = 0;
            Double angle1_temp, angle2_temp;

            for (int loop = 0; loop <= 90; loop++)
            {
                if (angles[loop] > anginc_long)
                {
                    angle1_temp = angles[loop - 1];
                    angle2_temp = angles[loop];

                    if (IAM_Table_Name == "Thermoflow 21, Novatec Biosol, Fresnel")
                    {
                        IAM_longitudinal_temp1 = IAM_longitudinal[loop - 1];
                        IAM_longitudinal_temp2 = IAM_longitudinal[loop];
                        IAMLongitudinal = interpMethod(angle1_temp, IAM_longitudinal_temp1, angle2_temp, IAM_longitudinal_temp2, anginc_long);

                        goto transversal;
                    }

                    else if (IAM_Table_Name == "Thermoflow 25, Novatec - Superheater (Fresnel)")
                    {
                        for (int loop2 = 0; loop2 <= 9; loop2++)
                        {
                            if (Convert.ToDouble(IAM_thermoflow25[loop2].GetValue(0)) > anginc_long)
                            {
                                IAM_longitudinal_temp1 = Convert.ToDouble(IAM_thermoflow25[loop2 - 1].GetValue(1));
                                IAM_longitudinal_temp2 = Convert.ToDouble(IAM_thermoflow25[loop2].GetValue(1));
                                IAMLongitudinal = interpMethod(Convert.ToDouble(IAM_thermoflow25[loop2 - 1].GetValue(0)), IAM_longitudinal_temp1, Convert.ToDouble(IAM_thermoflow25[loop2].GetValue(0)), IAM_longitudinal_temp2, anginc_long);

                                goto transversal;
                            }
                        }
                    }
                }
            }

            transversal:

            for (int loop1 = 0; loop1 <= 90; loop1++)
            {
                if (angles[loop1] > anginc_trans)
                {
                    angle1_temp = angles[loop1 - 1];
                    angle2_temp = angles[loop1];

                    if (IAM_Table_Name == "Thermoflow 21, Novatec Biosol, Fresnel")
                    {
                        IAM_transversal_temp1 = IAM_transversal[loop1 - 1];
                        IAM_transversal_temp2 = IAM_transversal[loop1];
                        IAMTransversal = interpMethod(angle1_temp, IAM_transversal_temp1, angle2_temp, IAM_transversal_temp2, anginc_trans);

                        goto salida;
                    }

                    else if (IAM_Table_Name == "Thermoflow 25, Novatec - Superheater (Fresnel)")
                    {
                        for (int loop2 = 0; loop2 <= 9; loop2++)
                        {
                            if (Convert.ToDouble(IAM_thermoflow25[loop2].GetValue(0)) > anginc_long)
                            {
                                IAM_transversal_temp1 = Convert.ToDouble(IAM_thermoflow25[loop2 - 1].GetValue(2));
                                IAM_transversal_temp2 = Convert.ToDouble(IAM_thermoflow25[loop2].GetValue(2));
                                IAMTransversal = interpMethod(Convert.ToDouble(IAM_thermoflow25[loop2 - 1].GetValue(0)), IAM_transversal_temp1, Convert.ToDouble(IAM_thermoflow25[loop2].GetValue(0)), IAM_transversal_temp2, anginc_trans);

                                goto salida;
                            }
                        }
                    }
                }
            }

            salida:

            AdmisibleError = 1;

            NumberRows = 50;
            Receiver_lengths = new Double[Convert.ToInt64(NumberOfSegments)];
            Temperature = new Double[Convert.ToInt64(NumberOfSegments)];
            ThermalLosses = new Double[Convert.ToInt64(NumberOfSegments)];
            PressureDrop = new Double[Convert.ToInt64(NumberOfSegments)];
            rho = new Double[Convert.ToInt64(NumberOfSegments)];
            velocity = new Double[Convert.ToInt64(NumberOfSegments)];
            Reynold_number = new Double[Convert.ToInt64(NumberOfSegments)];
            Dynamic_viscosity = new Double[Convert.ToInt64(NumberOfSegments)];
            Darcy = new Double[Convert.ToInt64(NumberOfSegments)];
            Density_Viscosity = new Double[Convert.ToInt64(NumberOfSegments)];

            CrossArea = Math.PI * Math.Pow(Diameter_Interior / 1000, 2) / 4;

            Actual_Mass_Flux = (m_dot_h / NumberRows) / CrossArea;

            if (Actual_Mass_Flux < Desired_Mass_Flux)
            {
                do
                {
                    if (Actual_Mass_Flux < Desired_Mass_Flux)
                    {
                        NumberRows = NumberRows - 1;
                    }

                    Actual_Mass_Flux = (m_dot_h / NumberRows) / CrossArea;

                } while ((Desired_Mass_Flux - Actual_Mass_Flux) > 1);
            }

            if (Actual_Mass_Flux > Desired_Mass_Flux)
            {
                do
                {
                    if (Actual_Mass_Flux > Desired_Mass_Flux)
                    {
                        NumberRows = NumberRows + 1;
                    }

                    Actual_Mass_Flux = (m_dot_h / NumberRows) / CrossArea;

                } while ((-Desired_Mass_Flux + Actual_Mass_Flux) > 1);
            }

            double contador_bucle = 0;

            begining:

            IAMOverall = IAMLongitudinal * IAMTransversal;
            FACTOR = NominalOpticalEfficiency * CleanlinessFactor * EndLossFactor * IAMOverall;
            SolarFieldTemperatureIncrement = (SolarFieldOutputTemperature - SolarFieldInletTemperature) / (NumberOfSegments - 1);

            //First Step
            temp1 = DNI * FACTOR;

            //Second Step and Third Step
            temp2 = (SolarFieldThermalEnergy * 1000) / temp1;

            //Fourth Step
            temp3 = (temp2 / CollectorApertureWidth) / NumberRows;

            ReflectorApertureArea = temp2;

            // Loop Begin
            do
            {
                ReflectorArea = ReflectorApertureArea;
                ReflectorLength = ReflectorApertureArea / CollectorApertureWidth;
                RowLength = ReflectorLength / NumberRows;
                temp4 = ReflectorApertureArea * FACTOR;

                Temperature[0] = SolarFieldInletTemperature;
                Temperature[1] = Temperature[0] + SolarFieldTemperatureIncrement;
                Temperature[2] = Temperature[1] + SolarFieldTemperatureIncrement;
                Temperature[3] = Temperature[2] + SolarFieldTemperatureIncrement;
                Temperature[4] = Temperature[3] + SolarFieldTemperatureIncrement;
                Temperature[5] = Temperature[4] + SolarFieldTemperatureIncrement;
                Temperature[6] = Temperature[5] + SolarFieldTemperatureIncrement;
                Temperature[7] = Temperature[6] + SolarFieldTemperatureIncrement;
                Temperature[8] = Temperature[7] + SolarFieldTemperatureIncrement;
                Temperature[9] = Temperature[8] + SolarFieldTemperatureIncrement;

                LengthIncrement = RowLength / (NumberOfSegments - 1);

                ThermalLosses[0] = LengthIncrement * (((CoefficientA1 * Temperature[1]) + (CoefficientA2 * Temperature[1] * Temperature[1] * Temperature[1] * Temperature[1])) / 1000);
                ThermalLosses[1] = LengthIncrement * (((CoefficientA1 * Temperature[2]) + (CoefficientA2 * Temperature[2] * Temperature[2] * Temperature[2] * Temperature[2])) / 1000);
                ThermalLosses[2] = LengthIncrement * (((CoefficientA1 * Temperature[3]) + (CoefficientA2 * Temperature[3] * Temperature[3] * Temperature[3] * Temperature[3])) / 1000);
                ThermalLosses[3] = LengthIncrement * (((CoefficientA1 * Temperature[4]) + (CoefficientA2 * Temperature[4] * Temperature[4] * Temperature[4] * Temperature[4])) / 1000);
                ThermalLosses[4] = LengthIncrement * (((CoefficientA1 * Temperature[5]) + (CoefficientA2 * Temperature[5] * Temperature[5] * Temperature[5] * Temperature[5])) / 1000);
                ThermalLosses[5] = LengthIncrement * (((CoefficientA1 * Temperature[6]) + (CoefficientA2 * Temperature[6] * Temperature[6] * Temperature[6] * Temperature[6])) / 1000);
                ThermalLosses[6] = LengthIncrement * (((CoefficientA1 * Temperature[7]) + (CoefficientA2 * Temperature[7] * Temperature[7] * Temperature[7] * Temperature[7])) / 1000);
                ThermalLosses[7] = LengthIncrement * (((CoefficientA1 * Temperature[8]) + (CoefficientA2 * Temperature[8] * Temperature[8] * Temperature[8] * Temperature[8])) / 1000);

                ThermalLossesTotal = ThermalLosses[0] + ThermalLosses[1] + ThermalLosses[2] + ThermalLosses[3] + ThermalLosses[4] + ThermalLosses[5] + ThermalLosses[6] + ThermalLosses[7];
                //ThermalLossesTotal = ThermalLossesTotal * 1.01;

                temp5 = (temp4 * DNI / NumberRows) / 1000;
                temp6 = temp5 - ThermalLossesTotal;
                temp7 = temp6 * NumberRows;
                ERROR = SolarFieldThermalEnergy - temp7;

                ReflectorApertureArea = ReflectorApertureArea + 1;

            } while (ERROR > AdmisibleError);

            //END-LOSSES FACTOR calculation: a=Focal_distance, w=CollectorApertureWidth/2 
            EndLossFactor = 1 - (Math.Tan(rad(anginc_long)) * (Focal_distance / RowLength));

            contador_bucle = contador_bucle + 1;

            if (contador_bucle < 10)
            {
                goto begining;
            }

            else
            {

            }

            //SOLAR FIELD PRESSURE DROP CALCULATIONS
            //Reynolds Number calculation
            for (int j = 0; j < 10; j++)
            {
                Caudal_per_row = m_dot_h / NumberRows;

                //Density calculation
                if (HTF == "Solar Salt")
                {
                    rho[j] = (-0.636 * (Temperature[j])) + 2090;
                }

                else if (HTF == "Hitec XL")
                {
                    rho[j] = (3e-6 * (Temperature[j]) * (Temperature[j])) - 0.8285 * (Temperature[j]) + 2240.3;
                }

                else if (HTF == "Therminol VP1")
                {
                    rho[j] = (-0.0008 * (Temperature[j]) * (Temperature[j])) - 0.6364 * (Temperature[j]) + 1074;
                }

                else if (HTF == "Syltherm_800")
                {
                    rho[j] = (-0.0007 * (Temperature[j]) * (Temperature[j])) - 0.7166 * (Temperature[j]) + 946.03;
                }

                else if (HTF == "Dowtherm_A")
                {
                    rho[j] = (-0.0008 * (Temperature[j]) * (Temperature[j])) - 0.6314 * (Temperature[j]) + 1068.6;
                }

                else if (HTF == "Therminol_75")
                {
                    rho[j] = (-0.0004 * (Temperature[j]) * (Temperature[j])) - 0.596 * (Temperature[j]) + 1090.4;
                }

                else if (HTF == "Liquid Sodium")
                {
                    rho[j] = 219 + 275.32 * (1 - ((Temperature[j]) / 2503.7)) + 511.58 * Math.Pow((1 - ((Temperature[j]) / 2503.7)), 0.5);
                }

                //Velocity calculation v = Q/(rho x A)
                velocity[j] = Caudal_per_row / (rho[j] * CrossArea);

                //Dynamic_Viscosity calculation
                if (HTF == "Solar Salt")
                {
                    Dynamic_viscosity[j] = (-0.000000000147388 * Math.Pow((Temperature[j]), 3)) + (0.000000228024134 * Math.Pow((Temperature[j]), 2)) - (0.000119957203979 * (Temperature[j])) + 0.022707419662049;
                }

                else if (HTF == "Hitec XL")
                {
                    Dynamic_viscosity[j] = 1000000 * Math.Pow((Temperature[j]), -3.315);
                }

                else if (HTF == "Therminol VP1")
                {
                    Dynamic_viscosity[j] = (0.0002 * Math.Pow((Temperature[j]), -1.15)) * rho[j];
                }

                else if (HTF == "Syltherm_800")
                {
                    Dynamic_viscosity[j] = (1.1629 * Math.Pow((Temperature[j]), -1.361));
                }

                else if (HTF == "Dowtherm_A")
                {
                    Dynamic_viscosity[j] = (0.2222 * Math.Pow((Temperature[j]), -1.216));
                }

                else if (HTF == "Therminol_75")
                {
                    Dynamic_viscosity[j] = (24.252 * Math.Pow((Temperature[j]), -1.943));
                }

                else if (HTF == "Liquid Sodium")
                {
                    Dynamic_viscosity[j] = Math.Pow(Math.E, (-6.4406 - 0.3958 * Math.Log(Temperature[j]) + (556.835 / (Temperature[j]))));
                }

                //Density_Viscority
                Density_Viscosity[j] = rho[j] / Dynamic_viscosity[j];

                //Reynold_number
                Reynold_number[j] = Density_Viscosity[j] * velocity[j] * (Diameter_Interior / 1000);

                double to, e1;

                int i = 0;
                Console.Write("Ingresar el valor estimado de x = ");

                Darcy[j] = 0.001;
                do
                {
                    to = Darcy[j];
                    Darcy[j] = Darcy[j] - funcion_Darcy(Darcy[j], Rugosidad, Diameter_Interior, Reynold_number[j]) / derivada_Darcy(Darcy[j], Rugosidad, Diameter_Interior, Reynold_number[j]);
                    e1 = Math.Abs((Darcy[j] - to) / Darcy[j]);
                    i = i + 1;

                } while (e1 > 0.00000000001 && i < 100);

                PressureDrop[j] = ((Darcy[j] * (rho[j] / 2) * (Math.Pow(velocity[j], 2) / (Diameter_Interior / 1000))) * LengthIncrement) / 100000;
            }

            Total_Pressure_Drop = 0;
            for (int z = 0; z < 10; z++)
            {
                Total_Pressure_Drop = Total_Pressure_Drop + PressureDrop[z];
            }

            SolarImpinging_path = ((ReflectorApertureArea * DNI) / NumberRows) / 1000;
            SolarEnergyAbsorbed_path = ((ReflectorApertureArea * FACTOR * DNI) / NumberRows) / 1000;
            Energyloss_path = ThermalLossesTotal;
            NetAbsorbed_path = SolarEnergyAbsorbed_path - Energyloss_path;
            NeatAbsorbed_Field = NetAbsorbed_path * NumberRows;
            Collector_Efficiency = (NetAbsorbed_path / SolarImpinging_path) * 100;
        }

        //Función para calcular el Coeficiente de Darcy
        public static double funcion_Darcy(double x, double rugosidad1, double diametro1, double reynold1)
        {
            double a = rugosidad1 / (3.7 * diametro1);
            double b = 2.51 / reynold1;
            return -2 * Math.Log10(a + b / Math.Pow(x, 0.5)) - 1.0 / Math.Pow(x, 0.5);
        }

        public static double derivada_Darcy(double x, double rugosidad2, double diametro2, double reynold2)
        {
            double a = rugosidad2 / (3.7 * diametro2);
            double b = 2.51 / reynold2;
            return (b / (a * Math.Pow(x, 1.5) + b * x)) + (1.0 / (2 * Math.Pow(x, 1.5)));
        }

        Double interpMethod(Double x0, Double y0, Double x1, Double y1, Double x)
        {
            return y0 * (x - x1) / (x0 - x1) + y1 * (x - x0) / (x1 - x0);
        }

        public Double rad(Double angolo)
        {
            Double rad1;
            rad1 = angolo * Math.PI / 180;
            return rad1;
        }

        public Double gradi(Double angolo1)
        {
            Double gradi1;
            gradi1 = angolo1 * 180 / Math.PI;
            return gradi1;
        }

    }    
}
